#!/usr/bin/env python3
"""
中国非物质文化遗产网爬虫
爬取民俗分类数据
"""
import requests
from bs4 import BeautifulSoup
import json
import time
import re
import os

BASE_URL = "https://www.ihchina.cn"
LIST_URL = f"{BASE_URL}/projects.html"

HEADERS = {
    "User-Agent": "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.0",
    "Accept": "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
    "Accept-Language": "zh-CN,zh;q=0.9,en;q=0.8",
}

# 节日关键词映射
FESTIVAL_MAP = {
    '春节': '春节',
    '元宵': '元宵节',
    '清明': '清明节',
    '端午': '端午节',
    '七夕': '七夕节',
    '中秋': '中秋节',
    '重阳': '重阳节',
    '腊八': '腊八节',
    '小年': '小年',
    '除夕': '除夕',
    '冬至': '冬至',
    '立夏': '立夏',
    '立春': '立春',
    '祭': '祭祀',
    '庙会': '庙会',
    '龙舟': '端午节',
    '月饼': '中秋节',
    '粽子': '端午节',
    '秧歌': '秧歌节',
    '舞龙': '舞龙节',
    '舞狮': '舞狮',
}

def infer_festival(name):
    """根据名称推断所属节日"""
    for key, val in FESTIVAL_MAP.items():
        if key in name:
            return val
    return "其他民俗"

def extract_region(name):
    """从项目名称提取地区"""
    # 匹配括号内的地区
    match = re.search(r'[（(](.+?)[)）]', name)
    if match:
        region = match.group(1)
        # 清洗地区名
        region = region.replace('省', '').replace('市', '').replace('自治区', '').replace('自治州', '')
        return region.strip()
    return "全国"

def clean_name(name):
    """清洗项目名称，去掉括号内容"""
    return re.sub(r'[（(].*?[)）]', '', name).strip()

def fetch_page(url, retries=3):
    """获取页面内容"""
    for i in range(retries):
        try:
            resp = requests.get(url, headers=HEADERS, timeout=30)
            resp.encoding = 'utf-8'
            if resp.status_code == 200:
                return resp.text
            print(f"  Retry {i+1}: status {resp.status_code}")
        except Exception as e:
            print(f"  Retry {i+1}: {e}")
        time.sleep(2)
    return None

def parse_list_page(html):
    """解析列表页"""
    soup = BeautifulSoup(html, 'html.parser')
    items = []
    
    # 尝试多种可能的选择器
    selectors = [
        '.project-item',
        '.list-item',
        '.item',
        '[class*="project"]',
        '[class*="item"]',
    ]
    
    elements = []
    for selector in selectors:
        elements = soup.select(selector)
        if elements:
            break
    
    # 如果没找到，尝试查找所有包含链接的div
    if not elements:
        elements = soup.find_all('a', href=re.compile(r'/project/|/projects/'))
        for a in elements:
            name = a.get_text(strip=True)
            if name and len(name) > 2:
                href = a.get('href', '')
                if href.startswith('http'):
                    url = href
                else:
                    url = BASE_URL + href if not href.startswith('/') else BASE_URL + href
                items.append({
                    'name': name,
                    'url': url,
                    'region': extract_region(name)
                })
    else:
        for elem in elements:
            a = elem.find('a') if elem.name != 'a' else elem
            if not a:
                continue
            name = a.get_text(strip=True)
            if not name or len(name) < 2:
                continue
            href = a.get('href', '')
            if not href:
                continue
            if href.startswith('http'):
                url = href
            else:
                url = BASE_URL + href if not href.startswith('/') else BASE_URL + href
            items.append({
                'name': name,
                'url': url,
                'region': extract_region(name)
            })
    
    return items

def parse_detail_page(html):
    """解析详情页"""
    soup = BeautifulSoup(html, 'html.parser')
    
    # 尝试多种内容选择器
    content_selectors = [
        '.content-detail',
        '.project-content',
        '.detail-content',
        '[class*="content"]',
        '[class*="detail"]',
        'article',
        '.main-content',
    ]
    
    content = ""
    for selector in content_selectors:
        elem = soup.select_one(selector)
        if elem:
            content = elem.get_text(strip=True)
            break
    
    # 如果没找到，取body文本
    if not content:
        body = soup.find('body')
        if body:
            content = body.get_text(strip=True)
    
    # 提取简介（前300字）
    description = content[:300].replace('\n', ' ').replace('\r', ' ').strip()
    
    return {
        'description': description,
        'full_content': content[:1000]  # 保留前1000字
    }

def fetch_all_items():
    """获取所有列表项"""
    all_items = []
    
    # 先尝试获取第一页
    print("Fetching list page...")
    html = fetch_page(LIST_URL)
    if not html:
        print("Failed to fetch list page")
        return []
    
    items = parse_list_page(html)
    print(f"Found {len(items)} items on first page")
    all_items.extend(items)
    
    # 尝试获取分页（简单尝试页码2-5）
    for page in range(2, 6):
        page_url = f"{LIST_URL}?page={page}"
        print(f"Fetching page {page}...")
        html = fetch_page(page_url)
        if not html:
            continue
        items = parse_list_page(html)
        if not items:
            break
        # 去重
        new_items = [item for item in items if item['url'] not in [i['url'] for i in all_items]]
        print(f"Found {len(new_items)} new items")
        all_items.extend(new_items)
        time.sleep(1)
    
    return all_items

def fetch_item_details(items):
    """获取每个项目的详情"""
    results = []
    for i, item in enumerate(items):
        print(f"[{i+1}/{len(items)}] Fetching: {item['name']}")
        html = fetch_page(item['url'])
        if html:
            detail = parse_detail_page(html)
            item.update(detail)
        else:
            item['description'] = ""
            item['full_content'] = ""
        
        results.append(item)
        time.sleep(0.5)  # 礼貌延迟
    
    return results

def transform_to_entries(items):
    """转换为项目数据格式"""
    entries = []
    
    for item in items:
        clean_title = clean_name(item['name'])
        festival = infer_festival(item['name'])
        
        entry = {
            "Title": clean_title,
            "Region": item['region'],
            "Festival": festival,
            "Description": item.get('description', '')[:500],
            "Status": "存续",
            "Source": f"中国非物质文化遗产网/{item['url']}",
            "CreatedAt": time.strftime("%Y-%m-%dT%H:%M:%SZ", time.gmtime())
        }
        entries.append(entry)
    
    return entries

def save_json(entries, filepath):
    """保存为JSON文件"""
    with open(filepath, 'w', encoding='utf-8') as f:
        json.dump(entries, f, ensure_ascii=False, indent=2)
    print(f"Saved {len(entries)} entries to {filepath}")

def main():
    print("=== 中国非遗网民俗数据爬虫 ===")
    
    # 获取列表
    items = fetch_all_items()
    if not items:
        print("No items found")
        return
    
    print(f"Total items: {len(items)}")
    
    # 获取详情
    items_with_detail = fetch_item_details(items)
    
    # 转换为条目格式
    entries = transform_to_entries(items_with_detail)
    
    # 保存
    output_file = 'data/seed/folklore_ihchina.json'
    os.makedirs(os.path.dirname(output_file), exist_ok=True)
    save_json(entries, output_file)
    
    # 同时生成SQL插入脚本
    sql_lines = []
    sql_lines.append("-- 中国非遗网民俗数据")
    sql_lines.append(f"-- 生成时间: {time.strftime('%Y-%m-%d %H:%M:%S')}")
    sql_lines.append(f"-- 数据条数: {len(entries)}")
    sql_lines.append("")
    
    for entry in entries:
        sql = f"""INSERT INTO FolkloreEntries (Title, Region, Festival, Description, Status, Source, CreatedAt) 
VALUES ('{entry['Title'].replace("'", "''")}', '{entry['Region'].replace("'", "''")}', '{entry['Festival']}', '{entry['Description'].replace("'", "''")}', '{entry['Status']}', '{entry['Source']}', '{entry['CreatedAt']}');"""
        sql_lines.append(sql)
    
    sql_file = 'data/seed/folklore_ihchina.sql'
    with open(sql_file, 'w', encoding='utf-8') as f:
        f.write('\n'.join(sql_lines))
    print(f"Saved SQL to {sql_file}")
    
    print("\nDone!")

if __name__ == '__main__':
    main()

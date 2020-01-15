import re
import requests
from bs4 import BeautifulSoup

data = requests.get(
    "https://hotelyar.com/hotel/1150/%D9%87%D8%AA%D9%84-%D8%A7%D8%B3%D9%BE%DB%8C%D9%86%D8%A7%D8%B3-%D9%BE%D8%A7%D9%84%D8%A7%D8%B3-%D8%AA%D9%87%D8%B1%D8%A7%D9%86")
soup = BeautifulSoup(data.text, 'html.parser')

leaderbord = soup.find('div', {'id': 'waypoint'})
tbody = leaderbord.find('tbody')

for tr in tbody.find_all('tr'):
    TypeRoom = tr.find_all('td')[0].find_all('span')[0].text.strip()
    Discount = tr.find_all('td')[1].text.strip()
    Capacity = tr.find_all('td')[2].text.strip()
    ExtraService = tr.find_all('td')[3].text.strip()
    BoardPrice = tr.find_all('td')[5].find_all('span')[0].text.strip()
    FinalPrice = tr.find_all('td')[5].find_all('span')[1].text.strip()
    print([TypeRoom, Discount, Capacity, ExtraService, BoardPrice,
           FinalPrice])

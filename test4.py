import math

class Rect:
    width = 0 # 長方形の横幅
    height = 0 # 長方形の高さ
    distance = 0 # 長方形の対角線の長さ
    def __init__(self, height, width):
        self.width = width
        self.height = height
        self.distance = width**2 + height**2
        
    def __str__(self):
        return f"縦:{self.height} 横:{self.width} 対角:{self.distance}"

def __comp_rect(r1, r2): # 長方形を比較したときの大小関係を数値で返す。引数は二つの長方形
    d = r1.distance - r2.distance # r1が大きいときは正の値を返し、r2が大きいときは負の値を返す。一致するときは0を返す
    if d == 0:
        if r1.height > r2.height:
            return 1
        elif r1.height < r2.height:
            return -1
        else:
            return 0
    else:
        return d

def __sort_rect_r(data): # 長方形のリストをクイックソートするための再帰関数
    if len(data) <= 1:
        return data
    
    pivot = data.pop(0)
    
    left = [x for x in data if __comp_rect(x, pivot) < 0]
    right = [x for x in data if __comp_rect(x, pivot) > 0]
    
    left = __sort_rect_r(left)
    right = __sort_rect_r(right)
    
    return left + [pivot] + right

def __find_next_rectangle(lst): # 与えられた長方形のリストのそれぞれの要素に対して一つ大きい長方形を検索する関数
    data = []
    for w1 in range(2, 151):
        for h1 in range(1, w1):
            data.append(Rect(h1, w1)) # 考えられるすべての長方形を生成
    print("ソート開始")
    sData = __sort_rect_r(data) # 長方形を大きさ順にソート
    print("ソート終了")
    # s = [x.distance for x in sData]
    # print(s)
    
    output = "" # 外部ファイルに出力する文字列
    
    for x in lst:
        lower = 0
        upper = len(sData) - 1
        while True:
            center = int((upper - lower)/2) + lower
            if __comp_rect(x,sData[center]) == 0:
                if center == len(sData) - 1:
                    # print("最大の四角である")
                    break
                # print(f"縦:{x.height} 横:{x.width} 対角:{x.distance} の次 縦:{sData[center + 1].height} 横:{sData[center + 1].width} 対角:{sData[center + 1].distance}")
                output += f"{sData[center + 1].height} {sData[center + 1].width}\n"
                break
            elif __comp_rect(x, sData[center]) < 0:
                upper = center - 1
            elif __comp_rect(x, sData[center]) > 0:
                lower = center + 1
    print(output)
    f = open("output.txt", "w")
    f.write(output)
    f.close()

def __read_txt():
    data = []
    f = open("input.txt", "r", encoding="UTF-8")
    txtDataList = f.readlines()
    for txtData in txtDataList:
        s = txtData.split(" ")
        h = int(s[0])
        w = int(s[1])
        if h == 0 and w == 0:
            break
        data.append(Rect(h,w))
    f.close()
    
    return data

lst = __read_txt()
__find_next_rectangle(lst)


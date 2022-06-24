# -*- coding: utf-8 -*-
from flask import Flask,request
app = Flask(__name__)
import uuid
import base64
import datetime
num = [1]
@app.route('/')
def hello_world():
    return 'Hello World!'
@app.route('/sample/sign',methods=['GET','POST'])
def sample_sign():
    if request.method == "GET":
        card = request.args.get("card_code")
    elif request.method == "POST":
        card=request.form['card_code']
    print("%s in"%card)
    return "ok"
@app.route('/sample/getid',methods=['GET','POST'])
def sample_getid():
    if request.method == "GET":
        id = "%s%03d"%(datetime.datetime.now().strftime("%Y%m%d"),num[0])
        num[0]+=1
    print("creat id:%s in"%id)
    return id
@app.route('/sample/dataSubmit',methods=['GET','POST'])
def data_submit():
    if request.method == "GET":
        sample = request.args.get("sample_code")
        index = request.args.get("index_code")
        val = request.args.get("check_val")
    elif request.method == "POST":
        sample=request.form['sample_code']
        index=request.form['index_code']
        val=request.form['check_val']
    print("%s %s %s"%(sample,index,val))
    return "ok"
if __name__ == '__main__':
    app.run(host="192.168.3.2",debug=True,port=999)
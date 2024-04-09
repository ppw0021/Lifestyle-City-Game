/*Launch Server
* Execute this to build
* docker build --platform linux/arm/v7 -t dec5star/backend:arm .
* 
* Then push to docker hub
* docker push dec5star/backend:arm
* 
* Execute this on Raspberry PI to pull
* docker pull dec5star/backend:arm
* 
* This will run, and port forward HTTPS port 443, if you would like to add HTTP, uncomment the corresponding line and add -p 80:80
* docker run -v /etc/letsencrypt/archive/penushost.ddns.net/:/cert -p 443:443 dec5star/backend:arm
docker run -v /etc/letsencrypt/archive/penushost.ddns.net/:/cert -p 443:443 dec5star/backend:arm64 for pi_five
*/

//How to run POSTGRES shell
//sudo -u postgres psql 

//install PG

const express = require('express');
const https = require('https');
const fs = require('fs');
const path = require('path');
const {Client} = require('pg');

const app = express();
const client = new Client({
    host: "192.168.1.41",
    user: "postgres",
    port: 5432,
    password: "k6vw*7YWP4",
    database: "test_erp"
})

client.connect();

/*client.query('SELECT * FROM CLIENTS', (err, res) => {
    if(!err){
        console.log(res.rows);
    } else {
        console.log(err.message);
    }
    client.end;
})*/

app.get("/", (req, res) => {
    res.json({ "testing": ["one", "two", "three"] })
})

app.get("/api", (req, res) => {
    res.json({ "users": ["userOne", "userTwo", "userThree"] })
    client.query('SELECT * FROM CLIENTS', (sqlerr, sqlres) => {
        if(!sqlerr){
            res.json(sqlres.rows);
        } else {
            res.json(sqlerr.message);
        }
        client.end;
    })
})

const sslServer = https.createServer(
    {
        //How to get trusted Certificate Authority Certificate
        //How to generate own SSL CA cert
        //https://www.youtube.com/watch?v=USrMdBF0zcg

        //Unofficial keys
        key: fs.readFileSync(path.join(__dirname, 'cert', 'key.pem')),
        cert: fs.readFileSync(path.join(__dirname, 'cert', 'cert.pem'))
        
        //Raspberry pi keys
        //cert: fs.readFileSync('/cert/fullchain1.pem'),
        //key: fs.readFileSync('/cert/privkey1.pem')
    },
    app
)

sslServer.listen(443, () => console.log('Hosted with CA approved SSL certificate at penushost.ddns.net, HTTPS: 443'));
//app.listen(80, () => console.log("HTTP: 80"));
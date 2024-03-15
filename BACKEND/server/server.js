const express = require('express');
const https = require('https');
//const path = require('path');
const fs = require('fs');

const app = express();

app.get("/", (req, res) => {
    res.json({ "testing": ["one", "two", "three"] })
})

app.get("/api", (req, res) => {
    res.json({ "users": ["userOne", "userTwo", "userThree"] })
})

const sslServer = https.createServer(
    {
        //How to get trusted Certificate Authority Certificate


        //How to generate own SSL CA cert
        //https://www.youtube.com/watch?v=USrMdBF0zcg
        //key: fs.readFileSync(path.join(__dirname, 'cert', 'key.pem')),
        //cert: fs.readFileSync(path.join(__dirname, 'cert', 'cert.pem'))

        cert: fs.readFileSync('/cert/fullchain1.pem'),
        key: fs.readFileSync('/cert/privkey1.pem')
    },
    app
)

sslServer.listen(443, () => console.log('HTTPS: 443'));
//app.listen(80, () => console.log("HTTP: 80"));
//app.listen(686, () => { console.log("Server started on port 686") })
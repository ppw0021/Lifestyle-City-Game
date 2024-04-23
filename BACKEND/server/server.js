/*Launch Server
* Execute this to build
docker build --platform linux/arm64 -t dec5star/backend:arm64 .
* 
* Then push to docker hub
docker push dec5star/backend:arm64
* 
* Execute this on Raspberry PI to pull
docker pull dec5star/backend:arm64
* 
* This will run, and port forward HTTPS port 443, if you would like to add HTTP, uncomment the corresponding line and add -p 80:80
* docker run -v /etc/letsencrypt/archive/penushost.ddns.net/:/cert -p 443:443 dec5star/backend:arm
docker run -v /etc/letsencrypt/archive/penushost.ddns.net/:/cert -p 443:443 dec5star/backend:arm64
*/

//How to run POSTGRES shell
//sudo -u postgres psql 

//install PG

//How to connect to psql
/*
psql -h localhost -U postgres -d test_erp
*/
// 



const express = require('express');
const https = require('https');
const fs = require('fs');
const path = require('path');
const {Client} = require('pg');
const bodyParser = require('body-parser');

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

// Parse JSON bodies
app.use(bodyParser.json());

app.post("/login", (req, res) => {
    const { username, password } = req.body;
    console.log("login post request for: " + username);
    client.query('SELECT user_id, username, sesh_token, level, coins FROM users WHERE username = \'' + username + '\' AND password = \'' + password + '\'', (sqlerr, sqlres) => {
        if(!sqlerr){
            //console.log(sqlres);
            if (sqlres.rowCount == 0)
            {
                //No rows, send response
                console.log("login failed (incorrect details): " + username);
                res.json({ response: "no_match"});
            }
            else
            {
                //This can only be called ONCE
                console.log("login success: " + username);
                res.json(sqlres.rows);
            }
        } else {
            console.log("sql_error for: " + username);
            res.json({ response: "sql_error"});
            //res.json(sqlerr.message);
        }
        client.end;
    })
})

app.post("/getbase", (req, res) => {
    
    const { sesh_id, user_id } = req.body;

    const getBaseQuery = 'SELECT bi.instance_id, bi.structure_id, bp.building_name, bi.x_pos, bi.y_pos, u.username AS owner_username FROM building_instances bi JOIN building_prefabs bp ON bi.structure_id = bp.structure_id JOIN buildings_owner bo ON bi.instance_id = bo.building_instance_id JOIN users u ON bo.base_owner_id = u.user_id WHERE u.user_id = \'' + user_id + '\'';
    console.log("getbase pos request for (sesh_id, username): " + sesh_id + user_id);
    client.query(getBaseQuery, (sqlerr, sqlres) => {

        if(!sqlerr){
            //console.log(sqlres);
            if (sqlres.rowCount == 0)
            {
                //No rows, send response
                console.log("no base for: " + user_id);
                res.json({ response: "no_base"});
            }
            else
            {
                //This can only be called ONCE
                console.log("base found for id: " + user_id);
                res.json(sqlres.rows);
            }
        } else {
            console.log("sql_error for (base): " + user_id);
            res.json({ response: "sql_error"});
            //res.json(sqlerr.message);
        }
        client.end;
    })

})

app.post("/updateuserproperty", (req, res) => {
    const { sesh_id, user_id, property_to_update, new_property_value} = req.body;
    const sqlStatment = (usrid, ptu, npv) => {
        return ('UPDATE "users" SET "' + ptu + '" = ' + npv + ' WHERE "user_id" = ' + usrid);
    }
    const sqlConfirmationStatement = (usrid, ptu, npv) => {
        return ('SELECT "' + ptu + '" FROM "users" WHERE "user_id" = ' + usrid)
    }
    console.log(sqlStatment(user_id, property_to_update, new_property_value));
    client.query(sqlStatment(user_id, property_to_update, new_property_value), (sqlerr, sqlres) => {
        if(!sqlerr){
            //console.log(sqlres);
            if (sqlres.rowCount == 0)
            {
                //No rows, send response
                console.log("did not complete coin change for: " + user_id);
                res.json({ response: "set_sql_failed"});
            }
            else
            {
                //This can only be called ONCE
                console.log("updated coins for: " + user_id);
                
                //This returns undefined, this is the point of the second query, to confirm that it worked
                //console.log(res.rows);
                client.query(sqlConfirmationStatement(user_id, property_to_update, new_property_value), (sql2err, sql2res) => {
                    if (!sql2err){
                        if (sql2res.rowCount == 0)
                        {
                            console.log("confirmation squery failed for: " + user_id);
                            res.json({ response: "conf_query_failed"});
                        }
                        else
                        {
                            const jsonObject = sql2res.body;
                            
                            console.log(sql2res.rows)
                            res.json({ response: "conf_query_success"});
                        }
                    }
                })
                //res.json(sqlres.rows);
            }
        } else {
            console.log("sql_error (coins) for: " + user_id);
            res.json({ response: "sql_error"});
            //res.json(sqlerr.message);
        }
        client.end;
    });
    console.log("update user property request for (sesh_id, username, property_to_update, new_property_value): " + sesh_id + user_id + property_to_update + new_property_value);
    client.end;
})

app.get("/api", (req, res) => {
    //res.json({ "users": ["userOne", "userTwo", "userThree"] })
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
        //key: fs.readFileSync(path.join(__dirname, 'cert', 'key.pem')),
        //cert: fs.readFileSync(path.join(__dirname, 'cert', 'cert.pem'))
        
        //Raspberry pi keys
        cert: fs.readFileSync('/cert/fullchain1.pem'),
        key: fs.readFileSync('/cert/privkey1.pem')
    },
    app
)

sslServer.listen(443, () => console.log('Hosted with CA approved SSL certificate at penushost.ddns.net, HTTPS: 443'));
//app.listen(80, () => console.log("HTTP: 80"));
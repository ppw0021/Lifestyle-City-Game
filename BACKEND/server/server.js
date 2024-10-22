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



const express = require('express')
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
    console.log("LOGIN REQUEST (username): " + username);
    client.query('SELECT user_id, username, sesh_token, level, coins, xp FROM users WHERE username = \'' + username + '\' AND password = \'' + password + '\'', (sqlerr, sqlres) => {
        if(!sqlerr){
            //console.log(sqlres);
            if (sqlres.rowCount == 0)
            {
                //No rows, send response
                console.log("   wrong login (username): " + username);
                res.json({ response: "no_match"});
            }
            else
            {
                //This can only be called ONCE
                console.log("   login success (username): " + username);
                res.json(sqlres.rows);
            }
        } else {
            console.log("   sql_error for: " + username);
            res.json({ response: "sql_error"});
            //res.json(sqlerr.message);
        }
        client.end;
    })
})

app.post("/getuser", (req, res) => {
    const { user_id } = req.body;
    console.log("GETUSER REQUEST (user_id): " + user_id);
    client.query('SELECT user_id, username, sesh_token, level, coins, xp FROM users WHERE user_id = \'' + user_id + '\'', (sqlerr, sqlres) => {
        if(!sqlerr){
            //console.log(sqlres);
            if (sqlres.rowCount == 0)
            {
                //No rows, send response
                console.log("   no match (user_id): " + user_id);
                res.json({ response: "no_match"});
            }
            else
            {
                //This can only be called ONCE
                console.log("   found user (user_id): " + user_id);
                res.json(sqlres.rows);
            }
        } else {
            console.log("   sql_error for: " + user_id);
            res.json({ response: "sql_error"});
            //res.json(sqlerr.message);
        }
        client.end;
    })
})

app.post("/updateuser", (req, res) => {
    const { user_id, sesh_id } = req.body;
    console.log("UPDATE USER REQUEST (user_id, sesh_id): " + user_id + ", " + sesh_id);
    client.query('SELECT user_id, username, answer, sesh_token, level, coins, xp FROM users WHERE user_id = \'' + user_id + '\'', (sqlerr, sqlres) => {
        if(!sqlerr){
            //console.log(sqlres);
            if (sqlres.rowCount == 0)
            {
                //No rows, send response
                console.log("   update user failed (user_id): " + user_id);
                res.json({ response: "no_match"});
            }
            else
            {
                //This can only be called ONCE
                console.log("   update user success (user_id): " + user_id);
                res.json(sqlres.rows);
            }
        } else {
            console.log("   sql_error for (user_id): " + user_id);
            console.log(sqlerr.message);
            res.json({ response: "sql_error"});
            //res.json(sqlerr.message);
        }
        client.end;
    })
})
/*
app.post("/placebuilding", (req, res) => {
    const { user_id, sesh_id, structure_id, x_pos, y_pos} = req.body;
    console.log("PLACE BUILDING REQUEST (user_id, sesh_id, structure_id, x_pos, y_pos): " + user_id + ", " + sesh_id + ", " +  structure_id + ", " + x_pos + ", " + y_pos);
    client.query("INSERT INTO building_instances (structure_id, x_pos, y_pos) " + "VALUES (" + structureId + ", " + xPos + ", " + yPos + ") " + "RETURNING instance_id;", (sqlerr, sqlres) => {
        if(!sqlerr){
            //console.log(sqlres);
            if (sqlres.rowCount == 0)
            {
                //No rows, send response
                console.log("   building place fail (user_id): " + user_id);
                res.json({ response: "no_match"});
            }
            else
            {
                //This can only be called ONCE
                console.log("   building place success (user_id): " + user_id);
                res.json(sqlres.rows);
            }
        } else {
            console.log("   sql_error for (user_id): " + user_id);
            console.log(sqlerr.message);
            res.json({ response: "sql_error"});
            //res.json(sqlerr.message);
        }
        client.end;
    })
})*/

app.post("/placebuilding", (req, res) => {
    const { user_id, sesh_id, structure_id, x_pos, y_pos } = req.body;
    console.log("PLACE BUILDING REQUEST (user_id, sesh_id, structure_id, x_pos, y_pos): " + user_id + ", " + sesh_id + ", " + structure_id + ", " + x_pos + ", " + y_pos);

    // Start a transaction
    client.query('BEGIN', (beginErr) => {
        if (beginErr) {
            console.error("Error beginning transaction:", beginErr.message);
            return res.json({ response: "sql_error" });
        }

        // Insert building owner first
        //console.log("SQL1: INSERT INTO building_instances (structure_id, x_pos, y_pos) VALUES (" + structure_id + "," + x_pos + "," + y_pos + ");");
        client.query("INSERT INTO building_instances (structure_id, x_pos, y_pos) VALUES (" + structure_id + "," + x_pos + "," + y_pos + ");",
            (ownerErr, ownerRes) => {
                if (ownerErr) {
                    console.error("Error inserting building owner:", ownerErr.message);
                    client.query('ROLLBACK', (rollbackErr) => {
                        if (rollbackErr) {
                            console.error("Error rolling back transaction:", rollbackErr.message);
                        }
                        return res.json({ response: "sql_error" });
                    });
                } else {
                    console.log("   Instance insert success (user_id): " + user_id);
                    //const bid = ownerRes.rows[0].building_instance_id;

                    // Insert building instance
                    //console.log("SQL2: INSERT INTO buildings_owner (base_owner_id, building_instance_id) VALUES (" + user_id + ", " + bid + ");");
                    client.query("INSERT INTO buildings_owner (base_owner_id, building_instance_id) VALUES (" + user_id + ", currval('building_instance_id_seq'));",
                        (instanceErr, instanceRes) => {
                            if (instanceErr) {
                                console.error("Error inserting building instance:", instanceErr.message);
                                client.query('ROLLBACK', (rollbackErr) => {
                                    if (rollbackErr) {
                                        console.error("Error rolling back transaction:", rollbackErr.message);
                                    }
                                    return res.json({ response: "sql_error" });
                                });
                            } else {
                                // Commit transaction
                                client.query('COMMIT', (commitErr) => {
                                    if (commitErr) {
                                        console.error("Error committing transaction:", commitErr.message);
                                        return res.json({ response: "sql_error" });
                                    }
                                    console.log("   Building place success (user_id): " + user_id);
                                    return res.json({ response: "success" });
                                });
                            }
                        });
                }
            });
    });
});

app.get("/getalluserids", (req, res) => {
    //const { user_id } = req.body;
    const query = "SELECT user_id FROM users;"
    console.log("GET USER_ID LIST REQUEST");
    client.query(query, (sqlerr, sqlres) => {

        if(!sqlerr){
            //console.log(sqlres);
            if (sqlres.rowCount == 0)
            {
                //No rows, send response
                console.log("   no usernames (user_id): ");
                res.json({ response: "no_usernames"});
            }
            else
            {
                //This can only be called ONCE
                console.log("   usernames found for (user_id): ");
                console.log(sqlres.rows);
                res.json(sqlres.rows);
            }
        } else {
            console.log("   sql_error for (user_id): ");
            res.json({ response: "sql_error"});
            //res.json(sqlerr.message);
        }
        client.end;
    })   
})

app.post("/adduser", (req, res) => {
    const {username, password, answer, sesh_token, coins} = req.body;
    const setQuery = "insert into users (username, password, answer, sesh_token, level, coins, xp) values ('" + username + "', '" + password + "', '" + answer + "', '" + sesh_token + "', 0, " + coins + ", 0);";
    console.log("ADD USER REQUEST: ('" + username + "', '" + password + "', '" + answer + "', '" + sesh_token + "', 0, " + coins + ", 0);");
    client.query(setQuery, (sqlerr, sqlres) => {
        if(!sqlerr){
            //console.log(sqlres);
            if (sqlres.rowCount == 0)
            {
                //No rows, send response
                console.log("   add user failed (username): " + username);
                res.json({ response: "no_match"});
            }
            else
            {
                //This can only be called ONCE
                console.log("   add user success (username): " + username);
                res.json({ response: "success"});
            }
        } else {
            console.log("   sql_error for (username): " + username);
            console.log(sqlerr.message);
            res.json({ response: "sql_error"});
            //res.json(sqlerr.message);
        }
        client.end;
    })
})

app.post("/getbase", (req, res) => {
    
    const { user_id } = req.body;

    const getBaseQuery = 'SELECT bi.instance_id, bi.structure_id, bp.building_name, bi.x_pos, bi.y_pos, u.username AS owner_username FROM building_instances bi JOIN building_prefabs bp ON bi.structure_id = bp.structure_id JOIN buildings_owner bo ON bi.instance_id = bo.building_instance_id JOIN users u ON bo.base_owner_id = u.user_id WHERE u.user_id = \'' + user_id + '\'';
    console.log("GETBASE REQUEST (user_id): " + user_id);
    client.query(getBaseQuery, (sqlerr, sqlres) => {

        if(!sqlerr){
            //console.log(sqlres);
            if (sqlres.rowCount == 0)
            {
                //No rows, send response
                console.log("   no base (user_id): " + user_id);
                res.json({ response: "no_base"});
            }
            else
            {
                //This can only be called ONCE
                console.log("   base found (user_id): " + user_id);
                res.json(sqlres.rows);
            }
        } else {
            console.log("   sql_error for (user_id): " + user_id);
            res.json({ response: "sql_error"});
            console.log(sqlerr.message);
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
    console.log("UPDATE USER PROP (user_id, property_to_update, new_property_value): " + user_id + ", " + property_to_update + ", " + new_property_value);
    //console.log(sqlStatment(user_id, property_to_update, new_property_value));
    client.query(sqlStatment(user_id, property_to_update, new_property_value), (sqlerr, sqlres) => {
        if(!sqlerr){
            //console.log(sqlres);
            if (sqlres.rowCount == 0)
            {
                //No rows, send response
                console.log("   failed (user_id): " + user_id);
                res.json({ response: "set_sql_failed"});
            }
            else
            {
                //This can only be called ONCE
                console.log("   updated property (user_id): " + user_id);
                
                //This returns undefined, this is the point of the second query, to confirm that it worked
                //console.log(res.rows);
                /*client.query(sqlConfirmationStatement(user_id, property_to_update, new_property_value), (sql2err, sql2res) => {
                    if (!sql2err){
                        if (sql2res.rowCount == 0)
                        {
                            console.log("       confirmation query failed (user_id): " + user_id);
                            res.json({ response: "conf_query_failed"});
                        }
                        else
                        {
                            const jsonObject = sql2res.body;
                            
                            console.log("       " + sql2res.rows)
                            res.json({ response: "conf_query_success"});
                        }
                    }
                })
                //res.json(sqlres.rows);*/
            }
        } else {
            console.log("   sql_error (coins) for: " + user_id);
            res.json({ response: "sql_error"});
            //res.json(sqlerr.message);
        }
        client.end;
    });
    //console.log("update user property request for (sesh_id, username, property_to_update, new_property_value): " + sesh_id + user_id + property_to_update + new_property_value);
    client.end;
})

app.post("/updateuserpassword", (req, res) => {
    const { username, property_to_update, new_property_value } = req.body;

    // Assuming property_to_update is always 'password'
    const sqlStatement = (username, newPassword) => {
        return 'UPDATE "users" SET "password" = \'' + newPassword + '\' WHERE "username" = \'' + username + '\'';
    };

    console.log("UPDATE USER PROP (username, property_to_update, new_property_value): " + username + ", " + property_to_update + ", " + new_property_value);

    // Using property_to_update as 'password'
    client.query(sqlStatement(username, new_property_value), (sqlerr, sqlres) => {
        if (!sqlerr) {
            if (sqlres.rowCount === 0) {
                console.log("   failed (username): " + username);
                res.json({ response: "set_sql_failed" });
            } else {
                console.log("   updated password for user: " + username);
                res.json({ response: "password_updated" });
            }
        } else {
            console.log("   sql_error for: " + username);
            res.json({ response: "sql_error" });
            console.log(sqlerr.message);
        }
        // Properly close the database connection
    });
});


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
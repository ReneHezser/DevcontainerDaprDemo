const express = require('express')
const bodyParser = require('body-parser')

const app = express()

// When Dapr sends a message it uses application/cloudevents+json
app.use(bodyParser.json({ type: 'application/*+json' }));

const port = 3000

// tell Dapr what we want to subscribe to Dapr does a GET to /dapr/subscribe to find out
app.get('/dapr/subscribe', (req, res) => {
    console.log('/dapr/subscribe has been called')
    res.json([{
        pubsubname: "pubsub",
        topic: "sampleTopic",
        route: "message"
    }]);
})

// Dapr will post to the route configured for the topic
app.post('/message', (req, res) => {
    console.log("message on /sampleTopic: ", req.body);

    // send 200 to indicate succesful receipt
    res.sendStatus(200);
});

app.listen(port, () => console.log(`consumer app listening on port ${port}!`))
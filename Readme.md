# Pubsub demo with RabitMQ / Service Bus
This sample is meant to show how to use dapr for message exchange, running in a devcontainer.
- https://docs.microsoft.com/en-us/dotnet/architecture/dapr-for-net-developers/publish-subscribe
- https://docs.dapr.io/developing-applications/building-blocks/pubsub/pubsub-overview/

## Special configuration for a devcontainer
With the devcontainer, dapr will be installed in a container and kicks off the other containers with a docker-compose file. The approach does not require to install dapr on the host.

Using the devcontainer needs some considerations that you need to be aware of.

- The docker compose file (*relevant parts only!*)
    ```yaml
    services:
        demodatagenerator:
            ...
            networks:
                - hello-dapr
        demodatagenerator-dapr:
            command: [
                "-app-id", "demodatagenerator",
                "-app-port", "3500",
                # Dapr's placement service can be reach via the docker DNS entry
                "-placement-host-address", "placement:50006",
                # override the default path to map the volume mapped directory
                "-components-path", "/components"
            ]
            volumes:
                # Mount our components folder into the devcontainer
                - ./.devcontainer/components/:/components 
            depends_on:
                - demodatagenerator
            ports:
                - 3500:3500
            networks:
                - hello-dapr
        
        demosubscriber-dapr:
            container_name: demo-subscriber_dapr
            command: [
                "-app-id", "demosubscriber",
                "-app-port", "3000",
            ]
            depends_on:
                - demosubscriber
            network_mode: "service:demosubscriber"

    networks: 
        hello-dapr:
            name: hello-dapr
    ```
- devcontainer.json (*relevant parts only!*)
    ```json
    {
        "dockerComposeFile": "docker-compose.yml",
        // Use 'forwardPorts' to make a list of ports inside the container available locally.
	    "forwardPorts": [3000,3500,5672,50006,15672],
    ```
    3000/3500 are for the apps, 5672/15672 are used by RabbitMQ, 50006 for dapr

1. assign the service and dapr container to the same network in the docker-compose file if the dapr sidecar needs to talk to your container --> **network_mode**
2. mount the components folder in the devcontainer file, as well as for each dapr container in the docker-compose file
3. configure used ports that need to be available via localhost in the devcontainer file (instead of the ports setting in the compose file)

# Links
- https://docs.dapr.io/developing-applications/building-blocks/pubsub/howto-publish-subscribe
- https://github.com/dapr/samples/tree/master/hello-docker-compose
- https://github.com/gbaeke/dapr-demo/tree/master/pubsub
- https://github.com/dapr/dapr/issues/2838#issuecomment-784459694
- https://www.youtube.com/watch?v=umrUlfrZqKk&t=2s
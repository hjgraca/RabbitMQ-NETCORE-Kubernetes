Build docker image

docker build -t send-demo .

Run in docker

docker run --name send-demo --rm -it -p 8000:80 send-demo

Push to registry

docker push yourhubusername/send-demo


Image in public registry
hjgraca/rabbitmq_send_demo


Kubernetes

kubectl create -f .\Kubernetes_Service.yaml

kubectl get services --namespace=test-rabbitmq

kubectl create -f .\Kubernetes_Deployment.yaml

kubectl get pods --namespace=test-rabbitmq --output=wide
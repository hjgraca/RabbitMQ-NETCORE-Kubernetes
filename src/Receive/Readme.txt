Build docker image

docker build -t receive-demo .

Run in docker

docker run --name receive-demo --rm -it receive-demo

Push to registry

docker push yourhubusername/receive-demo

Kubernetes

kubectl create secret generic rabbitmq-secret --from-literal=password='yourpassword' --namespace=test-rabbitmq

kubectl get secret rabbitmq-secret --namespace=test-rabbitmq

kubectl create -f .\Kubernetes_Deployment.yaml

kubectl get pods --namespace=test-rabbitmq --output=wide

kubectl logs worker-[id] --namespace=test-rabbitmq

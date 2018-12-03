#!/bin/bash

GIT_REP="https://gitlab.s2it.com.br/santa-helena/click-do-bem_api.git"
MYSQL_SERVER="sh-clickdobem.cs9qmxdrrnhg.us-east-1.rds.amazonaws.com"
MYSQL_DATABASE="clickdobemdev"
MYSQL_USER="shdev"
MYSQL_PASSWORD="sh.click"

echo "Preprando ambiente"
echo "--------------------------------------------------------------------------"
rm -rf /tmp/dev-clickdobem-api
cd /tmp

echo "Obtendo repositorio"
echo "--------------------------------------------------------------------------"

git clone $GIT_REP dev-clickdobem-api
cd dev-clickdobem-api
git checkout develop

CID=$(docker ps -a | grep dev-clickdobem-api | awk '{ print $1 }')
CIDSIZE=${#CID}
if [ $CIDSIZE -gt 0 ];
then
        echo "Removendo container"
        echo "--------------------------------------------------------------------------"
        docker rm dev-clickdobem-api -f
fi

IID=$(docker image list | grep santa-helena/dev-clickdobem-api | awk '{ print $3 }')
IIDSIZE=${#IID}
if [ $IIDSIZE -gt 0 ];
then
        echo "Removendo imagem"
        echo "--------------------------------------------------------------------------"
        docker rmi santa-helena/dev-clickdobem-api --force
fi

echo "Gerando Imagem"
echo "--------------------------------------------------------------------------"
docker build -t santa-helena/dev-clickdobem-api -f SantaHelena.ClickDoBem.Services.Api/Dockerfile .

echo "Iniciando Container"
echo "--------------------------------------------------------------------------"
docker run --name dev-clickdobem-api -e MYSQL_SERVER=$MYSQL_SERVER -e MYSQL_DATABASE=$MYSQL_DATABASE -e MYSQL_USER=$MYSQL_USER -e MYSQL_PASSWORD=$MYSQL_PASSWORD -p 5010:80 --restart always --volume /data/api/dev-images:/app/wwwroot/images -d santa-helena/dev-clickdobem-api

echo "Excluindo arquivos temporarios"
cd /
rm -rf /tmp/dev-clickdobem-api

echo "Finalizado"
#!/bin/bash

GIT_REP="https://gitlab.s2it.com.br/santa-helena/click-do-bem_api.git"
MYSQL_SERVER="sh-clickdobem.cs9qmxdrrnhg.us-east-1.rds.amazonaws.com"
MYSQL_DATABASE="clickdobemhml"
MYSQL_USER="shhml"
MYSQL_PASSWORD="sh.click.hml"

echo "Preprando ambiente"
echo "--------------------------------------------------------------------------"
rm -rf /tmp/hml-clickdobem-api
cd /tmp

echo "Obtendo repositorio"
echo "--------------------------------------------------------------------------"

git clone $GIT_REP hml-clickdobem-api
cd hml-clickdobem-api
git checkout homolog

CID=$(docker ps -a | grep hml-clickdobem-api | awk '{ print $1 }')
CIDSIZE=${#CID}
if [ $CIDSIZE -gt 0 ];
then
        echo "Removendo container"
        echo "--------------------------------------------------------------------------"
        docker rm hml-clickdobem-api -f
fi

IID=$(docker image list | grep santa-helena/hml-clickdobem-api | awk '{ print $3 }')
IIDSIZE=${#IID}
if [ $IIDSIZE -gt 0 ];
then
        echo "Removendo imagem"
        echo "--------------------------------------------------------------------------"
        docker rmi santa-helena/hml-clickdobem-api --force
fi

echo "Gerando Imagem"
echo "--------------------------------------------------------------------------"
docker build -t santa-helena/hml-clickdobem-api -f SantaHelena.ClickDoBem.Services.Api/Dockerfile .

echo "Iniciando Container"
echo "--------------------------------------------------------------------------"
docker run --name hml-clickdobem-api -e MYSQL_SERVER=$MYSQL_SERVER -e MYSQL_DATABASE=$MYSQL_DATABASE -e MYSQL_USER=$MYSQL_USER -e MYSQL_PASSWORD=$MYSQL_PASSWORD -p 5011:80 --restart always --volume /data/api/hml-images:/app/wwwroot/images -d santa-helena/hml-clickdobem-api

echo "Excluindo arquivos temporarios"
cd /
rm -rf /tmp/hml-clickdobem-api

echo "Finalizado"
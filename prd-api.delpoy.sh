#!/bin/bash

GIT_REP="https://gitlab.s2it.com.br/santa-helena/click-do-bem_api.git"
MYSQL_SERVER="sh-clickdobem.cs9qmxdrrnhg.us-east-1.rds.amazonaws.com"
MYSQL_DATABASE="clickdobemprd"
MYSQL_USER="sh-clickdobem"
MYSQL_PASSWORD="Cl1K-D0.3Em+5H"

echo "Preprando ambiente"
echo "--------------------------------------------------------------------------"
rm -rf /tmp/prd-clickdobem-api
cd /tmp

echo "Obtendo repositorio"
echo "--------------------------------------------------------------------------"

git clone $GIT_REP prd-clickdobem-api
cd prd-clickdobem-api
git checkout master

CID=$(docker ps -a | grep prd-clickdobem-api | awk '{ print $1 }')
CIDSIZE=${#CID}
if [ $CIDSIZE -gt 0 ];
then
        echo "Removendo container"
        echo "--------------------------------------------------------------------------"
        docker rm prd-clickdobem-api -f
fi

IID=$(docker image list | grep santa-helena/prd-clickdobem-api | awk '{ print $3 }')
IIDSIZE=${#IID}
if [ $IIDSIZE -gt 0 ];
then
        echo "Removendo imagem"
        echo "--------------------------------------------------------------------------"
        docker rmi santa-helena/prd-clickdobem-api --force
fi

echo "Gerando Imagem"
echo "--------------------------------------------------------------------------"
docker build -t santa-helena/prd-clickdobem-api -f SantaHelena.ClickDoBem.Services.Api/Dockerfile .

echo "Iniciando Container"
echo "--------------------------------------------------------------------------"
docker run --name prd-clickdobem-api -e MYSQL_SERVER=$MYSQL_SERVER -e MYSQL_DATABASE=$MYSQL_DATABASE -e MYSQL_USER=$MYSQL_USER -e MYSQL_PASSWORD=$MYSQL_PASSWORD -p 5010:80 --restart always --volume /data/api/prd-images:/app/wwwroot/images -d santa-helena/prd-clickdobem-api

echo "Excluindo arquivos temporarios"
cd /
rm -rf /tmp/prd-clickdobem-api

echo "Finalizado"
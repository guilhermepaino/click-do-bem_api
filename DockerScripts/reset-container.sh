#!/bin/bash
CONTAINERS=$(docker ps -a | grep -v sql | grep -v git | grep -v Up | awk '{ print $NF }' | grep -v NAMES)

for c in $CONTAINERS
do
        docker rm $c
done

IMAGES=$(docker image list | grep "<none>" | awk '{ print $3 }')

for i in $IMAGES
do
        docker rmi $i
done
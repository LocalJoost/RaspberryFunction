cd /d %~dp0
docker buildx build -t raspberryfunction:latest -f RaspberryFunction/Dockerfile.x64 -o type=docker,dest=raspberryfunctionx64.tar .
pause


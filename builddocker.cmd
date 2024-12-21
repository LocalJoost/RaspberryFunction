cd /d %~dp0
docker buildx build --platform linux/arm64 -t raspberryfunction:latest -f RaspberryFunction/Dockerfile -o type=docker,dest=raspberryfunction.tar .
pause


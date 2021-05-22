#!/bin/sh
sites='icanhazip.com ifconfig.co ifconfig.me api.ipify.org ' # wooledge.org/myip.cgi ipconfig.in/ip ipecho.net/plain'
for site in $sites;do
        ping -q -n -W0.5 -c2 "$site" > /dev/null
        if [ $? -eq 0 ];then
            wanip=$(curl -sS "$site")
            configip=$(grep -A2 wan configuration.yml | grep -oE "\b([0-9]{1,3}\.){3}[0-9]{1,3}\b")
            if [ "$wanip" != "$configip" ]; then
                echo "Wan IP has changed"
                sed -i -r "s|$configip|$wanip|g" configuration.yml
            fi
            break
            fi
    done

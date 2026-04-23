#!/usr/bin/env bash
set -euo pipefail

sudo apt-get update
sudo apt-get install -y nginx unzip

wget https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb -O /tmp/packages-microsoft-prod.deb
sudo dpkg -i /tmp/packages-microsoft-prod.deb
rm /tmp/packages-microsoft-prod.deb

sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0 aspnetcore-runtime-8.0

sudo mkdir -p /var/www/printsstudio/backend /var/www/printsstudio/frontend /var/www/printsstudio/data
sudo chown -R "$USER":"$USER" /var/www/printsstudio

echo "Ubuntu setup completed."

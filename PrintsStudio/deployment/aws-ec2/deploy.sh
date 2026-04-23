#!/usr/bin/env bash
set -euo pipefail

APP_ROOT=/var/www/printsstudio
BACKEND_OUT="$APP_ROOT/backend"
FRONTEND_OUT="$APP_ROOT/frontend"
TEMP_ROOT=/tmp/printsstudio-publish

sudo mkdir -p "$BACKEND_OUT" "$FRONTEND_OUT" "$APP_ROOT/data" "$TEMP_ROOT"
sudo chown -R "$USER":"$USER" "$APP_ROOT" "$TEMP_ROOT"

dotnet publish PrintsStudio.Server/PrintsStudio.Server.csproj -c Release -o "$TEMP_ROOT/backend"
dotnet publish PrintsStudio.Client/PrintsStudio.Client.csproj -c Release -o "$TEMP_ROOT/client"

cp -r "$TEMP_ROOT/backend/"* "$BACKEND_OUT/"
cp "$TEMP_ROOT/backend/appsettings.Production.json" "$BACKEND_OUT/appsettings.Production.json"
cp -r "$TEMP_ROOT/client/wwwroot/"* "$FRONTEND_OUT/"
sudo chown -R www-data:www-data "$BACKEND_OUT" "$APP_ROOT/data"

sudo cp deployment/aws-ec2/printsstudio.service /etc/systemd/system/printsstudio.service
sudo cp deployment/aws-ec2/nginx-printsstudio.conf /etc/nginx/sites-available/printsstudio
sudo ln -sf /etc/nginx/sites-available/printsstudio /etc/nginx/sites-enabled/printsstudio
sudo rm -f /etc/nginx/sites-enabled/default

sudo systemctl daemon-reload
sudo systemctl enable printsstudio
sudo systemctl restart printsstudio
sudo nginx -t
sudo systemctl restart nginx

echo "Deployment completed."

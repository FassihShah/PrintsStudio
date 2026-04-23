# AWS EC2 Demo Deployment

This app can be deployed on one small Ubuntu EC2 instance for demo use:

- `Nginx` serves the Blazor WebAssembly frontend
- `systemd` runs the ASP.NET Core backend on port `5000`
- `SQLite` stores demo data in one local file
- one public IP or domain serves everything

## Recommended EC2 Shape

- Ubuntu 24.04 LTS
- `t3.micro` if free tier is available in your account
- Security group inbound:
  - `22` from your IP only
  - `80` from `0.0.0.0/0`
  - `443` only if you later add SSL

## First-Time Server Setup

Copy this repo to the EC2 instance, then run:

```bash
cd ~/PrintsStudio
chmod +x deployment/aws-ec2/setup-ubuntu.sh
./deployment/aws-ec2/setup-ubuntu.sh
```

## Publish And Deploy

From the project root on the server:

```bash
cd ~/PrintsStudio
chmod +x deployment/aws-ec2/deploy.sh
./deployment/aws-ec2/deploy.sh
```

The site will then be available on:

```text
http://YOUR_EC2_PUBLIC_IP
```

## Production App Config

The backend uses [PrintsStudio.Server/appsettings.Production.json](../../PrintsStudio.Server/appsettings.Production.json).

Current production setup:

- `DatabaseProvider` is `Sqlite`
- the database file is `/var/www/printsstudio/data/printsstudio.db`
- frontend and backend are expected to run from the same origin

## GitHub Actions Deployment

The workflow file is:

- [deploy-ec2.yml](../../.github/workflows/deploy-ec2.yml)

Required GitHub repository secrets:

- `EC2_HOST`
- `EC2_USERNAME`
- `EC2_SSH_KEY`

Recommended order:

1. Commit the workflow and deployment files locally
2. Add the GitHub Actions secrets in GitHub
3. Push to `main`

If you push before adding secrets, the workflow may fail on the first run. After adding secrets, push again or rerun the failed workflow.

## Useful Commands

```bash
sudo systemctl status printsstudio
sudo journalctl -u printsstudio -n 100 --no-pager
sudo systemctl restart printsstudio
sudo systemctl restart nginx
curl http://127.0.0.1:5000/health
curl http://YOUR_EC2_PUBLIC_IP/health
```

## Notes

- This setup is intentionally optimized for demo simplicity, not heavy traffic.
- SQLite is a good fit for a showcase server with low write volume.
- If you later need production scale, move the database to RDS and add HTTPS with a domain plus Certbot.

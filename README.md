# PrintsStudio

PrintsStudio is a full-stack printing service web app built with `Blazor WebAssembly` on the frontend and `ASP.NET Core` on the backend. It supports product browsing, custom orders, designer flows, bookings, reviews, contact forms, and admin management.

## Stack

- Frontend: `Blazor WebAssembly`, `Bootstrap`, `.NET 8`
- Backend: `ASP.NET Core Web API`, `.NET 8`
- Data: `Entity Framework Core`
- Auth: `ASP.NET Core Identity`
- Demo deployment: `Ubuntu EC2`, `Nginx`, `systemd`, `SQLite`

## Project Structure

```text
PrintsStudio/
├── PrintsStudio.Client/
├── PrintsStudio.Server/
├── PrintsStudio.Infrastructure/
├── PrintsStudio.Application/
├── PrintsStudio.Domain/
├── deployment/aws-ec2/
└── .github/workflows/
```

## Auth Notes

- The seeded admin account is:
  - Email: `admin@printsstudio.com`
  - Password: `Admin@123`
- Public signup is limited to `Customer` and `Designer`
- Signup and login now return clearer validation/auth messages

## EC2 Demo Deployment

Deployment files are in [deployment/aws-ec2/README.md](./PrintsStudio/deployment/aws-ec2/README.md).

This setup is designed for:

- a single Ubuntu EC2 instance
- low traffic demo usage
- one domain/IP serving both frontend and backend
- `SQLite` for lightweight showcase hosting

## GitHub Actions CI/CD

The workflow file is:

- [deploy-ec2.yml](./PrintsStudio/.github/workflows/deploy-ec2.yml)

It deploys automatically on every push to `main`.

### What to add in GitHub

Create these repository secrets in:

`GitHub repo -> Settings -> Secrets and variables -> Actions -> New repository secret`

Add:

- `EC2_HOST`
  Example: `98.86.120.138`
- `EC2_USERNAME`
  Usually: `ubuntu`
- `EC2_SSH_KEY`
  Paste the full contents of your `.pem` file

### When to add the secrets

Add the secrets before you push the commit that contains the workflow to `main`.

Why:

- If you push the workflow first, GitHub Actions will start immediately
- If the secrets are missing, the deployment job will fail

Safe order:

1. Commit the code and workflow locally
2. Add the GitHub Actions secrets in the repo settings
3. Push to `main`

If you already pushed without secrets, that is fine too. Just add the secrets and push one more commit, or rerun the workflow.

## Notes

- I was not able to run a local `dotnet` build in this terminal environment because `dotnet` is not installed here.
- The GitHub Actions workflow is the easiest way to verify build and deployment after push.

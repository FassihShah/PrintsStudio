Set these GitHub repository secrets before pushing the workflow to `main` if you want the first deployment run to succeed:

- `EC2_HOST`
  Example: `98.86.120.138`

- `EC2_USERNAME`
  Usually: `ubuntu`

- `EC2_SSH_KEY`
  Paste the full contents of your `.pem` private key

Workflow file:

- [deploy-ec2.yml](./workflows/deploy-ec2.yml)

This workflow runs on every push to `main`, uploads the repo to EC2 over SSH, and then runs the deployment scripts on the server.

Recommended order:

1. Commit the workflow locally
2. Add the repository secrets in GitHub
3. Push to `main`

If you already pushed without secrets, add them now and either rerun the failed workflow or push one more commit.

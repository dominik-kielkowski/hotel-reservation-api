<h2>Hotel Reservation API</h2>
<p>A cloud-hosted booking website built with an ASP.NET backend and Angular frontend, allowing users to browse hotels and book rooms. The project is easily deployable to Azure using Terraform.

A GitHub Actions CI/CD pipeline builds and pushes a Docker image to Docker Hub, from where the production version is available for deployment in Azure Container Apps, while the local version is used for development and testing using Docker Compose.

The backend uses Redis Cache in the MediatR pipeline to speed up queries and Azure SQL for data storage. It also implements the Unit of Work pattern for atomic transactions and includes Unit Tests to ensure code quality.<p>

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/othneildrew/Best-README-Template">
    <img src="Documentation/images/logo.png" alt="Pharmacy Manager" height="80">
  </a>

  <p align="center">
    Full Stack project
  </p>
</div>



<!-- TABLE OF CONTENTS -->

<summary>Table of Contents</summary>
<ol>
  <li>
    <a href="#about-the-project">About The Project</a>
    <ul>
      <li><a href="#built-with">Built With</a></li>
    </ul>
  </li>
  <li>
    <a href="#getting-started">Getting Started</a>
    <ul>
      <li><a href="#prerequisites">Prerequisites</a></li>
      <li><a href="#installation">Installation</a></li>
    </ul>
  </li>
  <li><a href="#usage">Usage</a></li>
  <li><a href="#contributors">Contributors</a></li>
</ol>


<!-- ABOUT THE PROJECT -->
## About The Project

The system can be devided in two main parts. A public website is a market of medicines where any user can buy medicines from diferente pharmacies. 

The second part is a CMS (Content Managment System) where user with different user roles and privileges are able to manage pharamacies, medicines with their stock, create reports, between others.

The folowing are the main features segregated by the user role:

__Anonymous:__
* Search medicines by name or pharmacies with stock
* Buy medicines and follow the purchase state

__Admin:__
* Create/Edit registration invitations for any kind of user 
* Create new pharamacies

__Employee:__
* Create/Delete a medicine
* Create/List his requests of medicine stock
* List his pharmacy purchases
* Aprove/Reject purchases
* Export medicines to a json file 

__Owner:__
* Aprove/Reject requests of medicine stock 
* Generate purchases reports between dates


### Built With

The following technologies has been used in the backend:

* [![Net][Net.com]][Net-url]
* [![SQLServer][SQLServer.com]][Net-url]

In the frontend:
* [![Angular][Angular.io]][Angular-url]
* [![Bootstrap][Bootstrap.com]][Bootstrap-url]

For building and deploying the hole environment:
* [![Docker][Docker.com]][Net-url]


<!-- GETTING STARTED -->
## Getting Started

To get a local environment up and running follow these steps.


### Prerequisites

All the services, frontend, backend and data base are containerized with docker to make the bulding and deploying very simple. Make sure to have [docker](https://www.docker.com/) installed in your computer.


### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/cristhianfms/pharmacies.git
   ```
2. Go to the working directory
   ```sh
   cd project_folder/docker
   ```

3. Build docker images and run containers

   ```js
   docker-compose up -d 
   ```

4. In the browser access to http://localhost


<!-- USAGE EXAMPLES -->
## Usage

A default admin user is predifined with the credentials: 

`user: Admin`

`password: admin1234-`

<br />

![Product GIF][frontend-gif]

<br />


_For a complete API documentation, please refer to the [API Documentation](Documentation/API_Documentation.pdf)_


<!-- Contributors -->
## Contributors 
[![LinkedIn][linkedin-shield-cris]][linkedin-url-cris] [![LinkedIn][linkedin-shield-fede]][linkedin-url-fede] [![LinkedIn][linkedin-shield-nacho]][linkedin-url-nacho]


<!-- MARKDOWN LINKS & IMAGES -->
[linkedin-shield-cris]: https://img.shields.io/badge/-Cristhian_Maciel-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url-cris]: https://www.linkedin.com/in/cristhianfms/
[linkedin-shield-nacho]: https://img.shields.io/badge/-Ignacio_Olivera-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url-nacho]: https://www.linkedin.com/in/nacho/
[linkedin-shield-fede]: https://img.shields.io/badge/-Federico_Czarnievicz-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url-fede]: https://www.linkedin.com/in/federico-czarnievicz-907a28200/
[SQLServer.com]: https://img.shields.io/badge/SQLServer-CC2927?style=for-the-badge&logo=sqlserver&logoColor=white
[SQLServer-url]: https://www.microsoft.com/en-us/sql-server/
[NET.com]: https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=net&logoColor=white
[NET-url]: https://dotnet.microsoft.com/
[Angular.io]: https://img.shields.io/badge/Angular-DD0031?style=for-the-badge&logo=angular&logoColor=white
[Angular-url]: https://angular.io/
[Bootstrap.com]: https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white
[Bootstrap-url]: https://getbootstrap.com
[Docker.com]: https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=sqlserver&logoColor=white
[Docker-url]: hhttps://www.docker.com/
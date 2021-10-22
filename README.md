
![Logo](https://res.cloudinary.com/playwriteot/image/upload/v1632659052/logo_gtw0cv.png)

    
# Playwrite OT - Proposed Solution

This application is a simple, easy to use, low maintenance website. 
The website's main purpose is to provide guidelines to parents, provide 
activities to promote development and to offer support to caregivers and others
in the occupational therapy sphere. The website focuses on interactivity, accessible,
and ease of use for both the therapist and parent/caregiver. 


## Deployment
This project is deployed on Heroku who does not natively support .NET and C#. As such
Docker is used to run the application on Heroku's container registry. 
Therefore, to deploy the project, run the following commands in the project directory:

### 1. Create/Build the Dockerfile
Ensure that docker has been set up locally and is up to date, then build the image.
```bash
  docker build -t playwriteot:<Version> .   
```


### 2. Configure HerokuCLI
Ensure the [HerokuCLI](https://devcenter.heroku.com/articles/heroku-cli) has been installed and you have signed into the container registry.

```bash
  heroku login 
  heroku container:login
```

### 3. Push the image to Heroku
```bash
  heroku container:push web -a playwrite-ot   
```
   
### 4. Release/Deploy the image from Heroku 
```bash
 heroku container:release web -a playwrite-ot
```

  
## Authors

- [@JustinBailey](https://github.com/Justin-Bailey-Developer)
- [@YusufLimbada](https://github.com/yusuf-limbada)
- [@Neliswa](https://github.com/Neliswa30)
- [@CharmaineD](https://github.com/19003337)
- [@Japhet](https://github.com/Japhet-github)
- [@Rochelle](https://github.com/19006697)
- [@CaidenM](https://github.com/CaidanM)
- [@EmileJonkers](https://github.com/AlphaAemilius)

  

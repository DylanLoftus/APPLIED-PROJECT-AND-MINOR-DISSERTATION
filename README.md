# 3D House Simulator

## About this Project
The concept of this project is to see if Virtual Reality can be used as a medium for learning/training. We created a 3D house which simulates temperature flow between the rooms within the house and the outside world. We did this to answer our question about VR and it's capability to be used as a learning aid. The project was completed by Ronan Hanley and Dylan Loftus, as the "capstone" project to our software development course.

## Notable Repository Contents
**4th Year Project/**: Contains all of the Unity project files. We use Unity version *2019.2.13f1* for our project. Notably, the subdirectory *Assets/\__Scripts* contains all of the scripts we developed for the simulation.

**Dissertation/**: All of the files relating to the 42 page dissertation we completed as part of the project. This contains a PDF of the dissertation, and a *zip* file containing the LaTeX files. We did not complete the dissertation through git, as we wanted to use Overleaf. To remedy this, the introduction of our dissertation contains a breakdown of who completed which sections.

**Project Builds/**: Contains compiled builds of our project. Instructions for how to run these are outlined below.

**research/**: The initial reserach we conducted in order to complete the project. This also includes some of the planning we did for various aspects of the project.

**weather-api/**: All of the files relating to the RESTful weather API that we created for use with the project. This includes the OpenAPI specification, Flask server code, weather data files, and the scripts we used to process the weather data into a format that is appropriate for use in our project. Running any of this code is not required to assess the project, as the weather data is served in the background to each instance of our project over the internet, using our own server.

**ScreenCast.mp4**: A brief ~6 minute tour of our project. This was done over a video call, with each of us taking turns to explain various aspects of the project. 

## Running the Simulation
To run this project, navigate to the *Project Builds* folder. Within that folder you will find two *zip* archives: *PC Build* and *Oculus Build*. Both archives must be extracted before the project can be excecuted.

### PC Build
To run the PC Build, navigate to the *Project Builds* folder and extract *PC Build.zip*. Then run 4th Year Project.exe.

### Oculus Build
To run the Oculus Build, navigate to the *Project Builds* folder and extract *OculusBuild.zip*. Connect your Oculus Quest to your PC using the USB cable and make sure developer mode is turned on. Use [SideQuest](https://sidequestvr.com/) to mount the apk to the Oculus Quest by dragging and dropping the apk into SideQuest. Within the Oculus Quest go to the *Unknown Sources* tab and run *4th Year Project*.

### Compilation through the Unity Editor
As the Unity project files have been aggressively "gitignored" to make collaborating on this project as smooth as possible, opening and running the project through the Unity Editor may or may not be possible. This is one of the reasons why we have included compiled builds in this repository.

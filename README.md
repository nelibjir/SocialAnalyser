# SocialAnalyser

## About
A project, currently in process, where you can obtain statistics from a .txt file called dataset, which contains social network data 
about friendships between users. For now the only statistics are how many users the dataset has and how many friends in average a 
single user has.

## How to use
The user have to upload a .txt file in a format where the first is ID of a user and then ID of its friend separated by space.
These couple of numbers are separated by newline. Id can be any string and can be repeated.

For instance : <br />
"0 2 <br />
 1 2 <br />
 1 3 <br />
 " <br />
 
After uploading the file and naming the dataset is possible to submit and so upload the file to databse. After that the user
can see a new button with given name of the dataset as have been submited. After a clik on this button user will see statistics
about the dataset.
 
You will need to clone this repository and open it as asp .net core project and run it on your computer as IIS server. 
FE is running on Angular, you will run it with command ng serve adn it will listen on adress: http://localhost:4200

The database is generating by itself.

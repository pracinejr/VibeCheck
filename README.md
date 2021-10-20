# VibeCheck - Fullstack

## Getting Started

1. Pull down this repo

1. Run the two scripts that are in the SQL folder. These will create the VibeCheck database and add some test data. The database it creates is identitical to the prototype from the last MVC sprint, except now we're capturing the `FirebaseUserId` in the UserProfile table

1. Everyone on the team should create their own Firebase project. **Each team member** should do the follow steps in the firebase console:

   - Go to [Firebase](https://console.firebase.google.com/u/0/) and add a new project. You can name it whatever you want (VibeCheck is a good name)
   - Go to the Authentication tab, click "Set up sign in method", and enable the Username and Password option.
   - Add at least two new users in firebase. Use email addresses that you find in the UserProfile table of your SQL Server database
   - Once firebase creates a UID for these users, copy the UID from firebase and update the `FirebaseUserId` column for the same users in your SQL Server database.
   - Click the Gear icon in the sidebar to go to Project Settings. You'll need the information on this page for the next few steps

1. Go to the `appSettings.Local.json.example` file. Replace the value for FirebaseProjectId with your own

1. Rename the `appSettings.Local.json.example` file to remove the `.example` extension. This file should now just be called `appSettings.Local.json`

1. Open your `client` directory in VsCode. Open the `.env.local.example` file and replace `__YOUR_API_KEY_HERE__` with your own firebase Web API Key

1. Rename the `.env.local.example` file to remove the `.example` extension. This file should now just be called `.env.local`

1. Install your dependencies by running `npm install` from the same directory as your `package.json` file

## Mock-Ups

The company has hired a designer and here are the mock-ups they provided.

> **NOTE:** Styling should **_NOT_** be prioritized over functionality. UI/UX **is** important, but it's more important that you focus on the code.

> **NOTE:** (The quill logo seen in some of the mockups is provided for you inside of the images folder along with the other mock-ups to use as needed!)

### Colors

These are the hex codes for the colors used in the mockups

* Red: `#db534b`
* Grey: `#6c767d`
* Black: `#343a40`
* Green: `#5bb8a6`

### Login Page

![Login Page](Images/VibeCheck-Login.PNG)

### Home Page

![Home Page](Images/VibeCheck_Home.png)

### Explore Page

![Explore Page](Images/VibeCheck_Explore.png)

### Your Post's Page

![Your Post's Page](Images/VibeCheck_MyPosts.png)

### Create Post Form

![Create Post Form](Images/VibeCheck_CreatePost.png)

### Post Details Page

![Post Details Page](Images/VibeCheck-PostDetails.PNG)

### Category List Page

![Category List Page](Images/VibeCheck-Lists.PNG)
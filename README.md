# PostAppRepo

Change value of "baseurl" in web.config

Sample Request: 
1. Create Post (Post) https://localhost:44337/api/Post?
  {
    "Id":"asd",
    "Name":"asda",
    "MakeBy":"sad",
    "MakeDate":"2017-08-01"
  }
  
2. Create Comment (Post) https://localhost:44337/api/Comment/Create?
  {
    "Id":"cmt1",
    "Name":"cmt1",
    "PostId":"asd",
    "UpVote":0,
    "DownVote":0,
    "MakeBy":"sad",
    "MakeDate":"2017-08-01"
  }

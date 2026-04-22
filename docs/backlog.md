User must be able to log into the service
– User will enter their credentials into Web Application
– Temporarily lock out user if they fail to enter correct credentials
– Web Application will connect to API to validate credentials
∗ Validate User Name and password
∗ Validate user’s active flag in database is set to true
– Upon validation, Backend requests a One Time Password
∗ Initial Login shouldn’t require OTP, but force user to set up OTP upon logging in
∗ MFA built into the API.
– User will provide OTP to Web App
– Web App will send OTP to API
– API will validate OTP, returns token to web app, web app moves user to their view.
• User will be taken to Dashboard view corresponding to their UserType
– Dashboard views will be simple and easy to navigate
– User has an attribute on the database called userType that can be set to ”Admin”,
”EdRep”, ”Accountant”, ”Repair Tech”, and ”Manager”.
– User will have different permissions based upon their userType
• Admin
– Admin can interact with all different functions of SID2
– All functions should have a corresponding userType for to use function and a &&
userType=”Admin” just so Admin can interact with functions
– See all visits planned for the day in their home view, sorted by the Edrep, and listed
chronologically
∗ (GET) Get Visits function in API grabs all visits corresponding to the given day
– Can click into any given visit to be taken to that school’s information view
∗ (GET) Get School function in api gets information pertaining from the school from
the School Info table in database
Palen Music Center - SiD2 Page 5
– Admin can create new user
∗ (POST)Create User function in api receives information from web app and creates
new entry on user table in database
– Admin can create new school
∗ (POST)Create School function in api receives information from web app and creates
new entry on user table in database
– Admin can create new stores
∗ (POST)Create Store function in api receives information from web app and creates
new entry on user table in database
– Admin can assign Edrep to stores
∗ (POST) edit EdRep to change an EdRep’s store id
– Admin can assign Edrep to schools
∗ (POST) edit EdRep to change an EdRep’s assignment id
• EdRep
– Their view should list their visits for that given day, listed chronologically
– Can click into any given visit to be taken to that school’s information view
• Accountant
– Has 2 tabs
∗ VPU
· Should see All Schools
· Can click into school to display school information
· Can add VPUs to school
∗ Repos
• Repair Tech
– Their View should list repairs in their system that are not finished
• Manager
– See all visits planned for the day for EdReps based in their Store, sorted by EdRep
and listed chronologically
– Can Click into any given visit to be taken to that school’s information view
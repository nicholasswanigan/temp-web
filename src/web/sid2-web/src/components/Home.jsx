import React, { useEffect } from 'react';
import AdminHome from './AdminHome';

function Home(){
    const user= JSON.parse(localStorage.getItem('user'));

    useEffect(() => {
        if (!user){
            //redirect to login if no user found
            window.location.href='/'
            return null
        }
    },[user]);

    //route to correct home page based on userType
    switch(user.userType?.toLowerCase()){
        case 'admin':
            return<AdminHome user={user} />
            default:
                return <div>Unknown User Type</div>;
    }
}

export default Home;
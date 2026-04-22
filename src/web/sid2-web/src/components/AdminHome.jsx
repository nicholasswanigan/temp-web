import React, { useState } from 'react';
import SidePanel from './SidePanel';
import ManageUsers from './ManageUsers';
import '../styles/Home.css';

function AdminHome({ user }) {
    const [currentView, setCurrentView] = useState('home');

    const navLinks = [
        { label: 'Home', onClick: () => setCurrentView('home') },
        { label: 'Manage Users', onClick: () => setCurrentView('users') },
        //{ label: 'System Settings', onClick: () => setCurrentView('settings') },
        //{ label: 'Reports', onClick: () => setCurrentView('reports') },
    ];

    const handleLogout = () => {
        localStorage.removeItem('user');
        localStorage.removeItem('token');
        window.location.href = '/';
    };

    const renderContent = () => {
        switch (currentView) {
            case 'users':
                return <ManageUsers />;
            case 'dashboard':
                return (
                    <>
                        <h1>Admin Dashboard</h1>
                        <p>Welcome to the admin dashboard. Select an option from the menu.</p>
                    </>
                );
            default:
                return <p>View: {currentView} - Coming soon...</p>;
        }
    };

    return (
        <div className="home-layout">
            <SidePanel user={user} navLinks={navLinks} onLogout={handleLogout} />
            <main className="main-content">
                <h1>Welcome {user.firstName} {user.lastName}</h1>
                {renderContent()}
            </main>
        </div>
    );
}

export default AdminHome;
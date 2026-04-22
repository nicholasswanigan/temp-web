import React from 'react';
import '../styles/SidePanel.css';

function SidePanel({ user, navLinks, onLogout }) {
    return (
        <div className="side-panel">
            <div className="panel-header">
                <h2>{user.userType} Panel</h2>
            </div>
            
            <div className="nav-links">
                {navLinks.map((link, index) => (
                    <button 
                        key={index} 
                        className="nav-links-btn"
                        onClick={link.onClick}
                    >
                        {link.label}
                    </button>
                ))}
            </div>
            
            <button className="logout-btn" onClick={onLogout}>
                Logout
            </button>
        </div>
    );
}

export default SidePanel;
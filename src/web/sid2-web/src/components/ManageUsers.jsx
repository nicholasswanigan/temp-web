import React, { useState } from 'react';

function ManageUsers() {
    const [view, setView] = useState('menu');
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');
    
    // Form state
    const [formData, setFormData] = useState({
        userType: '',
        name: '',
        firstName: '',
        lastName: '',
        email: '',
        phone: '',
        username: '',
        password: ''
    });

    const buttonStyle = {
        // backgroundColor: '#2458CA',
        color: 'white',
        padding: '1rem 2rem',
        border: 'none',
        borderRadius: '4px',
        cursor: 'pointer',
        fontSize: '1rem',
        margin: '0.5rem',
        transition: 'all 0.2s ease'
    };

    const inputStyle = {
        width: '100%',
        padding: '0.5rem',
        border: '1px solid #ddd',
        borderRadius: '4px',
        fontSize: '1rem',
        marginBottom: '1rem'
    };

    const labelStyle = {
        display: 'block',
        marginBottom: '0.5rem',
        fontWeight: '500'
    };

    const handleViewUsers = async () => {
        setLoading(true);
        setError('');
        setView('viewUsers');

        try {
            const token = localStorage.getItem('token');
            const response = await fetch('http://localhost:5135/api/user/getusers', {
                headers: {
                    'Authorization': `Bearer ${token}`,
                },
            });

            const data = await response.json();

            if (response.ok && data.status) {
                setUsers(data.users || []);
            } else {
                setError(data.message || 'Failed to load users');
            }
        } catch (err) {
            setError('Network error. Please try again.');
            console.error('Error fetching users:', err);
        } finally {
            setLoading(false);
        }
    };

    const handleCreateUser = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError('');
        setSuccess('');

        try {
            const token = localStorage.getItem('token');
            const response = await fetch('http://localhost:5135/api/user', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`,
                },
                body: JSON.stringify(formData),
            });

            const data = await response.json();

            if (response.ok && data.status) {
                setSuccess(`User created successfully! User ID: ${data.userId}`);
                // Reset form
                setFormData({
                    userType: '',
                    name: '',
                    firstName: '',
                    lastName: '',
                    email: '',
                    phone: '',
                    username: '',
                    password: ''
                });
            } else {
                setError(data.message || 'Failed to create user');
            }
        } catch (err) {
            setError('Network error. Please try again.');
            console.error('Error creating user:', err);
        } finally {
            setLoading(false);
        }
    };

    const handleInputChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    };

    const handleBack = () => {
        setView('menu');
        setError('');
        setSuccess('');
        setUsers([]);
    };

    return (
        <div style={{ zIndex: 10, position: 'relative' }}>
            {view === 'menu' && (
                <>
                    <h2>Manage Users</h2>
                    <div style={{ marginTop: '2rem' }}>
                        <button 
                            style={buttonStyle}
                            onMouseEnter={(e) => {
                                e.target.style.transform = 'translateY(-1px)';
                                e.target.style.boxShadow = '0 4px 8px rgba(0,0,0,0.2)';
                            }}
                            onMouseLeave={(e) => {
                                e.target.style.transform = 'translateY(0)';
                                e.target.style.boxShadow = 'none';
                            }}
                            onClick={handleViewUsers}
                        >
                            View All Users
                        </button>
                        <button 
                            style={buttonStyle}
                            onMouseEnter={(e) => {
                                e.target.style.transform = 'translateY(-1px)';
                                e.target.style.boxShadow = '0 4px 8px rgba(0,0,0,0.2)';
                            }}
                            onMouseLeave={(e) => {
                                e.target.style.transform = 'translateY(0)';
                                e.target.style.boxShadow = 'none';
                            }}
                            onClick={() => setView('createUser')}
                        >
                            Create New User
                        </button>
                    </div>
                </>
            )}

            {view === 'viewUsers' && (
                <>
                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '1rem' }}>
                        <h2>All Users</h2>
                        <button 
                            style={{...buttonStyle, padding: '0.5rem 1rem'}}
                            onMouseEnter={(e) => {
                                e.target.style.transform = 'translateY(-1px)';
                                e.target.style.boxShadow = '0 4px 8px rgba(0,0,0,0.2)';
                            }}
                            onMouseLeave={(e) => {
                                e.target.style.transform = 'translateY(0)';
                                e.target.style.boxShadow = 'none';
                            }}
                            onClick={handleBack}
                        >
                            ← Back
                        </button>
                    </div>

                    {loading && <p>Loading users...</p>}
                    {error && <div style={{ color: '#d32f2f', background: '#ffebee', padding: '1rem', borderRadius: '4px' }}>{error}</div>}
                    
                    {!loading && !error && users.length === 0 && (
                        <p>No users found.</p>
                    )}
                    
                    {!loading && users.length > 0 && (
                        <table style={{ width: '100%', borderCollapse: 'collapse', backgroundColor: 'white', boxShadow: '0 2px 4px rgba(0,0,0,0.1)' }}>
                            <thead>
                                <tr style={{ backgroundColor: '#f5f5f5' }}>
                                    <th style={{ padding: '1rem', textAlign: 'left', borderBottom: '2px solid #ddd' }}>ID</th>
                                    <th style={{ padding: '1rem', textAlign: 'left', borderBottom: '2px solid #ddd' }}>Name</th>
                                    <th style={{ padding: '1rem', textAlign: 'left', borderBottom: '2px solid #ddd' }}>Email</th>
                                    <th style={{ padding: '1rem', textAlign: 'left', borderBottom: '2px solid #ddd' }}>Phone</th>
                                    <th style={{ padding: '1rem', textAlign: 'left', borderBottom: '2px solid #ddd' }}>User Type</th>
                                </tr>
                            </thead>
                            <tbody>
                                {users.map((user) => (
                                    <tr key={user.id} style={{ borderBottom: '1px solid #e0e0e0' }}>
                                        <td style={{ padding: '1rem' }}>{user.id}</td>
                                        <td style={{ padding: '1rem' }}>{user.name}</td>
                                        <td style={{ padding: '1rem' }}>{user.email}</td>
                                        <td style={{ padding: '1rem' }}>{user.phone || 'N/A'}</td>
                                        <td style={{ padding: '1rem' }}>{user.type}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    )}
                </>
            )}

            {view === 'createUser' && (
                <>
                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '1rem' }}>
                        <h2>Create New User</h2>
                        <button 
                            style={{...buttonStyle, padding: '0.5rem 1rem'}}
                            onMouseEnter={(e) => {
                                e.target.style.transform = 'translateY(-1px)';
                                e.target.style.boxShadow = '0 4px 8px rgba(0,0,0,0.2)';
                            }}
                            onMouseLeave={(e) => {
                                e.target.style.transform = 'translateY(0)';
                                e.target.style.boxShadow = 'none';
                            }}
                            onClick={handleBack}
                        >
                            ← Back
                        </button>
                    </div>

                    {error && <div style={{ color: '#d32f2f', background: '#ffebee', padding: '1rem', borderRadius: '4px', marginBottom: '1rem' }}>{error}</div>}
                    {success && <div style={{ color: '#27ae60', background: '#e8f8f5', padding: '1rem', borderRadius: '4px', marginBottom: '1rem' }}>{success}</div>}

                    <form onSubmit={handleCreateUser} style={{ backgroundColor: 'white', padding: '2rem', borderRadius: '8px', boxShadow: '0 2px 10px rgba(0,0,0,0.1)', maxWidth: '600px' }}>
                        <div>
                            <label style={labelStyle}>User Type</label>
                            <select 
                                name="userType" 
                                value={formData.userType}
                                onChange={handleInputChange}
                                style={inputStyle}
                                required
                            >
                                <option value="Admin">Admin</option>
                                <option value="EdRep">Education Representative</option>
                                <option value="Accountant">Accountant</option>
                                <option value="Repair">Repair</option>
                                <option value="Manager">Manager</option>
                            </select>
                        </div>

                        <div>
                            <label style={labelStyle}>Full Name</label>
                            <input 
                                type="text"
                                name="name"
                                value={formData.name}
                                onChange={handleInputChange}
                                style={inputStyle}
                                required
                            />
                        </div>

                        <div>
                            <label style={labelStyle}>First Name</label>
                            <input 
                                type="text"
                                name="firstName"
                                value={formData.firstName}
                                onChange={handleInputChange}
                                style={inputStyle}
                            />
                        </div>

                        <div>
                            <label style={labelStyle}>Last Name</label>
                            <input 
                                type="text"
                                name="lastName"
                                value={formData.lastName}
                                onChange={handleInputChange}
                                style={inputStyle}
                            />
                        </div>

                        <div>
                            <label style={labelStyle}>Email</label>
                            <input 
                                type="email"
                                name="email"
                                value={formData.email}
                                onChange={handleInputChange}
                                style={inputStyle}
                                required
                            />
                        </div>

                        <div>
                            <label style={labelStyle}>Phone</label>
                            <input 
                                type="tel"
                                name="phone"
                                value={formData.phone}
                                onChange={handleInputChange}
                                style={inputStyle}
                            />
                        </div>

                        <div>
                            <label style={labelStyle}>Username</label>
                            <input 
                                type="text"
                                name="username"
                                value={formData.username}
                                onChange={handleInputChange}
                                style={inputStyle}
                                required
                            />
                        </div>

                        <div>
                            <label style={labelStyle}>Password</label>
                            <input 
                                type="password"
                                name="password"
                                value={formData.password}
                                onChange={handleInputChange}
                                style={inputStyle}
                                required
                            />
                        </div>

                        <button 
                            type="submit"
                            style={{...buttonStyle, width: '100%', margin: 0}}
                            disabled={loading}
                            onMouseEnter={(e) => {
                                if (!loading) {
                                    e.target.style.transform = 'translateY(-1px)';
                                    e.target.style.boxShadow = '0 4px 8px rgba(0,0,0,0.2)';
                                }
                            }}
                            onMouseLeave={(e) => {
                                e.target.style.transform = 'translateY(0)';
                                e.target.style.boxShadow = 'none';
                            }}
                        >
                            {loading ? 'Creating User...' : 'Create User'}
                        </button>
                    </form>
                </>
            )}
        </div>
    );
}

export default ManageUsers;
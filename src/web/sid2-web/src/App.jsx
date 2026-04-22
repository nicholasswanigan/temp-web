import { useState} from 'react';
import Login from './components/Login';
import Home from './components/Home';
import './App.css';

function App() {
    const [isLoggedIn, setIsLoggedIn] = useState(() => {
        // Initialize state based on localStorage
        const user = localStorage.getItem('user');
        const token = localStorage.getItem('token');
        return !!(user && token);
    });

    const handleLoginSuccess = () => {
        setIsLoggedIn(true);
    };

    return (
        <div className="App">
            {isLoggedIn ? <Home /> : <Login onLoginSuccess={handleLoginSuccess} />}
        </div>
    );
}

export default App;

import { BrowserRouter, Route, Routes, Link } from "react-router-dom";
import Home from "./pages/Home";
import CreatePost from "./pages/CreatePost";
import Post from "./pages/Post";
import Registeration from "./pages/Registeration";
import Login from "./pages/Login";
import PageNotFound from "./pages/PageNotFound";
import Profile from "./pages/Profile";
import ChangePassword from "./pages//ChangePassword";

import { AuthContext } from "./helpers/AuthContext";
import { useState, useEffect } from "react";
import axios from "axios";

function App() {
  const [authState, setAuthState] = useState({
    username: "",
    id: 0,
    status: false,
  });

  useEffect(() => {
    axios
      .get("https://posting-server.onrender.com/auth/auth", {
        headers: { accessToken: localStorage.getItem("accessToken") },
      })
      .then((response) => {
        if (response.data.error) setAuthState({ ...authState, status: false });
        else {
          setAuthState({
            username: response.data.username,
            id: response.data.id,
            status: true,
          });
        }
      });
  }, []);

  const logout = () => {
    localStorage.removeItem("accessToken");
    setAuthState({ username: "", id: 0, status: false });
  };

  return (
    <div>
      <AuthContext.Provider value={{ authState, setAuthState }}>
        <BrowserRouter>
          <div className="flex  px-4 bg-slate-800 mb-6 justify-between">
            {!authState.status ? (
              <div className="flex gap-4 justify-start">
                <Link to="/login" className="flex  justify-center my-4">
                  login
                </Link>
                <Link to="/Registeration" className="flex  justify-center my-4">
                  Registeration
                </Link>
              </div>
            ) : (
              <div className="flex gap-4 justify-start">
                <Link to="/" className="flex  justify-center my-4">
                  Home Page
                </Link>
                <Link to="/createpost" className="flex  justify-center my-4">
                  Create A Post
                </Link>
              </div>
            )}
            <div className="flex gap-4 left-0">
              {authState.status && <button onClick={logout}>logout</button>}
              <h1 className="flex items-center text-lg font-semibold">
                {authState.username}
              </h1>
            </div>
          </div>
          <Routes>
            <Route path="/" exact element={<Home />} />
            <Route path="/createpost" element={<CreatePost />} />
            <Route path="/post/:id" element={<Post />} />
            <Route path="/login" element={<Login />} />
            <Route path="/registeration" element={<Registeration />} />
            <Route path="*" element={<PageNotFound />} />
            <Route path="/profile/:id" element={<Profile />} />
            <Route path="/changepassword" element={<ChangePassword />} />
          </Routes>
        </BrowserRouter>
      </AuthContext.Provider>
    </div>
  );
}

export default App;

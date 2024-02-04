import { useState, useContext } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../helpers/AuthContext";

function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const { setAuthState } = useContext(AuthContext);

  let navigate = useNavigate();

  const login = () => {
    const data = { username: username, password: password };
    axios
      .post("http://localhost:5045/api/User/Login", data)
      .then((response) => {
        if (response.data.error) alert(response.data.error);
        else {
          setAuthState({
            username: response.data.username,
            id: response.data.id,
            status: true,
          });

          localStorage.setItem("accessToken", response.data.token);
          navigate("/");
        }
      });
  };
  return (
    <div className="grid justify-center gap-3">
      Username
      <input
        type="text"
        className="input input-bordered w-full max-w-xs"
        onChange={(event) => {
          setUsername(event.target.value);
        }}
      ></input>
      Password
      <input
        type="password"
        className="input input-bordered w-full max-w-xs"
        onChange={(event) => {
          setPassword(event.target.value);
        }}
      ></input>
      <button className="btn btn-active btn-ghost" onClick={login}>
        Login
      </button>
    </div>
  );
}

export default Login;

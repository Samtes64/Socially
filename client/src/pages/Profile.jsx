import { useParams } from "react-router-dom";
import axios from "axios";
import { useEffect, useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../helpers/AuthContext";

export default function Profile() {
  let { id } = useParams();
  const [username, setUsername] = useState("");
  const [listOfPosts, setListOfPosts] = useState([]);
  let navigate = useNavigate();
  const { authState } = useContext(AuthContext);

  useEffect(() => {
    axios
      .get(`https://posting-server.onrender.com/auth/basicinfo/${id}`)
      .then((response) => {
        setUsername(response.data.username);
      });
    axios
      .get(`https://posting-server.onrender.com/posts/byuserId/${id}`)
      .then((response) => {
        setListOfPosts(response.data);
      });
  }, []);
  return (
    <div className="">
      <div className="">
        <h1>Username: {authState.username}</h1>
        {authState.username === username && (
          <button
            onClick={() => {
              navigate("/changepassword");
            }}
          >
            change password
          </button>
        )}
      </div>
      <div className="items-center flex flex-col">
        {listOfPosts.map((value) => {
          return (
            <div
              key={value.id} // Make sure to add a unique key for each element in the list
              className="w-96 h-72 rounded-lg flex flex-col mt-7 border-[1px] border-slate-400 shadow-lg hover:cursor-pointer"
            >
              {" "}
              <div
                onClick={() => {
                  navigate(`/post/${value.id}`);
                }}
                className=" flex-[20%] border-b-slate-400 border-b-2 grid items-center justify-center bg-blue-500 text-white "
              >
                {value.title}
              </div>
              <div
                onClick={() => {
                  navigate(`/post/${value.id}`);
                }}
                className="flex-[60%] items-center justify-center grid"
              >
                {value.postText}
              </div>
              <div className="pl-5 flex-[20%] border-b-slate-400 border-b-2 items-center  bg-blue-500 text-white flex justify-between px-3">
                {value.username}
                <label>{value.Likes.length} </label>
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}

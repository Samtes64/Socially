import { useContext, useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import { AuthContext } from "../helpers/AuthContext";

function Post() {
  let { id } = useParams();
  const [postObject, setPostObject] = useState({});
  const [comments, setComments] = useState([]);
  const [newComment, setNewComment] = useState("");
  const { authState } = useContext(AuthContext);
  let navigate = useNavigate();

  const addComment = () => {
    axios
      .post(
        "http://localhost:5045/api/comments",
        {
          Text: newComment,
          PostId: id,
          Username: authState.username,
        },
        {
          headers: {
            accessToken: localStorage.getItem("accessToken"),
          },
        }
      )
      .then((response) => {
        if (response.data.error) alert(response.data.error);
        else {
          const commentToAdd = {
            text: newComment,
            username: response.data.username,
          };
          setComments([...comments, commentToAdd]);
          setNewComment("");
        }
      });
  };

  const deleteComment = (commentId) => {
    axios
      .delete(`http://localhost:5045/api/comments/${commentId}`, {
        
      })
      .then(() => {
        setComments(
          comments.filter((val) => {
            return val.id != commentId;
          })
        );
      });
  };

  const deletePost = (PostId) => {
    axios
      .delete(`http://localhost:5045/api/Post/${PostId}`, {
        headers: { accessToken: localStorage.getItem("accessToken") },
      })
      .then(() => {
        navigate(`/`);
      });
  };

  useEffect(() => {
    axios.get(`http://localhost:5045/api/Post/${id}`).then((response) => {
      setPostObject(response.data);
    });

    axios.get(`http://localhost:5045/api/comments/${id}`).then((response) => {
      setComments(response.data);
    });
  }, []);

  const editPost = (option) => {
    if (option === "title") {
      let newTitle = prompt("Enter New Title");
      axios.put(
        `http://localhost:5045/api/Post/title`,
        {
          newTitle: newTitle,
          id: id,
        },
        { headers: { accessToken: localStorage.getItem("accessToken") } }
      );
      setPostObject({ ...postObject, title: newTitle });
    } else {
      let newPostText = prompt("Enter new body:");
      axios.put(
        `http://localhost:5045/api/Post/postText`,
        {
          newText: newPostText,
          id: id,
        },
        { headers: { accessToken: localStorage.getItem("accessToken") } }
      );
      setPostObject({ ...postObject, postText: newPostText });
    }
  };

  return (
    <div className="flex justify-center items-center px-10 gap-10">
      <div
        key={postObject.id} // Make sure to add a unique key for each element in the list
        className="w-96 h-72 rounded-lg flex  flex-col mt-7 border-[1px] border-slate-400 shadow-lg "
      >
        {" "}
        <div
          className=" flex-[20%] border-b-slate-400 border-b-2 grid items-center justify-center bg-blue-500 text-white "
          onClick={() => {
            if (authState.username === postObject.username) {
              editPost("title");
            }
          }}
        >
          {postObject.title}
        </div>
        <div
          className="flex-[60%] items-center justify-center grid"
          onClick={() => {
            if (authState.username === postObject.username) {
              editPost("body");
            }
          }}
        >
          {postObject.textBody}
        </div>
        <div className="px-5 flex-[20%] border-b-slate-400 border-b-2 flex items-center justify-between bg-blue-500 text-white">
          <div>{postObject.username}</div>
          {authState.username === postObject.username && (
            <div>
              <button
                className="text-red-600 text-sm"
                onClick={() => deletePost(postObject.id)}
              >
                Delete Post
              </button>
            </div>
          )}
        </div>
      </div>

      <div>
        <div>
          <input
            type="text"
            value={newComment}
            placeholder="Comment ..."
            onChange={(event) => {
              setNewComment(event.target.value);
            }}
            className="input input-bordered input-info w-full max-w-xs mb-4"
          />
          <button onClick={addComment} className="btn glass">
            Add comment
          </button>
        </div>
        <div>
          {comments.map((comment, key) => {
            return (
              <div key={key} className="m-4 border p-5 rounded-md ">
                {comment.text}
                <div className="justify-end flex text-slate-700">
                  User: {comment.username}
                </div>
                {authState.username === comment.username && (
                  <button
                    onClick={() => {
                      deleteComment(comment.id);
                    }}
                    className="text-red-400 text-xs"
                  >
                    X
                  </button>
                )}
              </div>
            );
          })}
        </div>
      </div>
    </div>
  );
}

export default Post;

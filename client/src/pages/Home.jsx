import axios from "axios";
import { useEffect, useState } from "react";
// import {useHistory} from "react-router-dom"
import { useNavigate } from "react-router-dom";

function Home() {
  const [listOfPosts, setListOfPosts] = useState([]);
  // const [likedPosts, setLikedPosts] = useState([]);
  let navigate = useNavigate();

  useEffect(() => {
    if (!localStorage.getItem("accessToken")) {
      navigate(`/login`);
    } else {
      axios
        .get("http://localhost:5045/api/Post", {
          headers: { accessToken: localStorage.getItem("accessToken") },
        })
        .then((response) => {
          setListOfPosts(response.data);
          // setLikedPosts(
          //   response.data.likedPosts.map((like) => {
          //     return like.PostId;
          //   })
          // );
        });
    }
  }, []);

  // const likeAPost = (postId) => {
  //   axios
  //     .post(
  //       "https://posting-server.onrender.com/likes",
  //       { PostId: postId },
  //       { headers: { accessToken: localStorage.getItem("accessToken") } }
  //     )
  //     .then((response) => {
  //       setListOfPosts(
  //         listOfPosts.map((post) => {
  //           if (post.id === postId) {
  //             if (response.data.liked) {
  //               return { ...post, Likes: [...post.Likes, 0] };
  //             } else {
  //               const likesArray = post.Likes;
  //               likesArray.pop();
  //               return { ...post, Likes: likesArray };
  //             }
  //           } else {
  //             return post;
  //           }
  //         })
  //       );

  //       if (likedPosts.includes(postId)) {
  //         setLikedPosts(
  //           likedPosts.filter((id) => {
  //             return id != postId;
  //           })
  //         );
  //       } else {
  //         setLikedPosts([...likedPosts, postId]);
  //       }
  //     });
  // };

  return (
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
              {value.textBody}
            </div>
            <div className="pl-5 flex-[20%] border-b-slate-400 border-b-2 items-center  bg-blue-500 text-white flex justify-between px-3">
              <div
                onClick={() => {
                  navigate(`/profile/${value.UserId}`);
                }}
              >
                {value.username}
              </div>
              <div className="flex gap-3 ">
                <button
                // onClick={() => {
                //   likeAPost(value.id);
                // }}
                >
                  <i
                  // className={
                  //   likedPosts.includes(value.id)
                  //     ? "uil uil-thumbs-up text-blue-800"
                  //     : "uil uil-thumbs-up text-white"
                  // }
                  ></i>
                </button>
                {/* <div>{value.Likes.length}</div> */}
              </div>
            </div>
          </div>
        );
      })}
    </div>
  );
}

export default Home;

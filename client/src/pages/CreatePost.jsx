import { useEffect } from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";
import axios from "axios";
import { useNavigate } from "react-router-dom";

function CreatePost() {
  const initialValues = {
    title: "",
    postText: "",
  };

  useEffect(() => {
    if (!localStorage.getItem("accessToken")) {
      navigate(`/login`);
    }
  }, []);

  const validateSchema = Yup.object().shape({
    title: Yup.string().required("you must input a title"),
    postText: Yup.string().required(),
  });

  const onSubmit = (data) => {
    axios
      .post("https://posting-server.onrender.com/posts", data, {
        headers: { accessToken: localStorage.getItem("accessToken") },
      })
      .then(() => {
        navigate(`/`);
      });
  };

  let navigate = useNavigate();
  return (
    <div className=" flex justify-center">
      <Formik
        initialValues={initialValues}
        onSubmit={onSubmit}
        validationSchema={validateSchema}
      >
        <Form className="flex w-80 flex-col gap-4 border-slate-600 border-2 p-8">
          <label>Title: </label>
          <ErrorMessage
            className="alert alert-error"
            name="title"
            component="span"
          />
          <Field
            id="inputCreatePost"
            name="title"
            placeholder="(Ex. Title...)"
            className="border-slate-300 border-[1px] p-1"
          />
          <label>Post: </label>
          <ErrorMessage
            className="alert alert-error"
            name="postText"
            component="span"
          />
          <Field
            id="inputCreatePost"
            name="postText"
            placeholder="(Ex. Post...)"
            className="border-slate-300 border-[1px] p-1"
          />

          <button
            type="submit"
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
          >
            Create Post{" "}
          </button>
        </Form>
      </Formik>
    </div>
  );
}

export default CreatePost;

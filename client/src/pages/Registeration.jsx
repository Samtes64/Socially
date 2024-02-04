import React from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";
import axios from "axios";

function Registeration() {
  const initialValues = {
    username: "",
    password: "",
  };

  const validateSchema = Yup.object().shape({
    username: Yup.string().min(3).max(15).required(),
    password: Yup.string().min(4).max(20).required(),
  });

  const onSubmit = (data) => {
    axios.post("http://localhost:5045/api/User/Register", data).then(() => {
      console.log(data);
    });
  };

  return (
    <div className=" flex justify-center">
      <Formik
        initialValues={initialValues}
        onSubmit={onSubmit}
        validationSchema={validateSchema}
      >
        <Form className="flex w-80 flex-col gap-4 border-slate-600 border-2 p-8">
          <label>Username: </label>
          <ErrorMessage
            className="alert alert-error"
            name="username"
            component="span"
          />
          <Field
            id="inputCreateUser"
            name="username"
            placeholder="(Ex. Abebe123...)"
            className="border-slate-300 border-[1px] p-1"
          />

          <label>Password: </label>
          <ErrorMessage
            className="alert alert-error"
            name="password"
            component="span"
          />
          <Field
            id="inputCreateUser"
            type="password"
            name="password"
            placeholder="password"
            className="border-slate-300 border-[1px] p-1"
          />

          <button
            type="submit"
            className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
          >
            Register{" "}
          </button>
        </Form>
      </Formik>
    </div>
  );
}

export default Registeration;

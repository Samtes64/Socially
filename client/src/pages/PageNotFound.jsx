
import { Link } from "react-router-dom";

function PageNotFound() {
  return (
    <div className="text-5xl grid justify-center gap-5">
      <h1>Page Not Found</h1>
      <h1 className="text-xl">
        Go to home Page : <Link className="text-blue-600" to="/">Home Page</Link>
      </h1>
    </div>
  );
}

export default PageNotFound;

import { Route, Redirect } from "react-router-dom";


function ProtectedRoute({ component: Component, ...rest }) {


	return (
		<Route
			{...rest}
			render={(props) => {
				return <Component {...props} />;
			}}
		/>
	);
}

export default ProtectedRoute;

import "./App.css";

import { BrowserRouter as Router, Switch, Route } from "react-router-dom";

import Navbar from "./components/Navbar";

import ProtectedRoute from "./pages/ProtectedRoute";

import Products from "./pages/Products";
import ProductDetail from "./pages/ProductDetail";
import Basket from "./pages/Basket";
import Admin from "./pages/Admin";
import Error404 from "./pages/Error404";
import ProductsByCategory from "./pages/ProductsByCategory";

function App() {
	return (
		<Router>
			<div>
				<Navbar />
				
				<div id="content">
					<Switch>
						<Route path="/" exact component={Products} />
						<Route path="/product/:product_id" component={ProductDetail} />
						<Route path="/category/:category_id" component={ProductsByCategory} />
						<Route path="/basket" component={Basket} />
						<ProtectedRoute path="/admin" component={Admin} admin={true} />
						<Route path="*" component={Error404} />
					</Switch>
				</div>
			</div>
		</Router>
	);
}

export default App;

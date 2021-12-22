import styles from "./styles.module.css";
import { Link } from "react-router-dom";
import { Button } from "@chakra-ui/react";
import { useQuery } from "react-query";
import { fetchCategoryList, postProduct, postCategory } from "../../api";

import { useBasket } from "../../contexts/BasketContext";

function Navbar() {
	const { items } = useBasket();

	const { data, 
			errors, 
			status } = useQuery("categories", fetchCategoryList)
	
	if (status === "loading") return "Loading...";
	if (status === "error") return "An error has occurred: " + errors.message;

	return (
		<>
		<nav className={styles.nav}>
			<div className={styles.left}>
				<div className={styles.logo}>
					<Link to="/">eCommerce</Link>
				</div>

				<ul className={styles.menu}>
					<li>
						<Link to="/">Products</Link>
					</li>
				</ul>
			</div>

			<div className={styles.right}>
				{items.length > 0 && (
					<Link to="/basket">
						<Button colorScheme="pink" variant="outline">
							Basket ({items.length})
						</Button>
					</Link>
				)}

					<Link to="/admin">
						<Button colorScheme="yellow" variant="ghost">
							Admin
						</Button>
					</Link>
			</div>
		</nav>
		<nav className={styles.nav}>
			<div className={styles.left}>
					<ul className={styles.menu}>
						{data.map((category, i) => (
							<li key={i}>
								<Link to={`/category/${category.id}`}>{category.name}</Link>
							</li>
						))}
					</ul>
				</div>
		</nav>				
		</>
	);
}

export default Navbar;

import React, { useState } from "react";

import { Grid, Box, Button, Center, Radio, RadioGroup, Stack } from "@chakra-ui/react";
import { useQuery } from "react-query";

import { fetchProductList } from "../../api";
import Card from "../../components/Card";

function Products() {
	const {
		data,
		error,
		status,
	} = useQuery("products", fetchProductList);

	const [sort, setSort] = useState(false)
	
	if (status === "loading") return "Loading...";
	if (status === "error") return "An error has occurred: " + error.message;

	const handleState = () => {
		setSort(!sort)
	};

	let dataInit = data.slice()
	let mapData=dataInit.sort(function(a, b) { return a.price - b.price })
	
	if (sort) {
		return (
			<div>
				<img src="https://cdn.dsmcdn.com/ty264/campaign/banners/original/592650/c170761659_0_new.jpg" width="100%" alt="" /> 
				<Center>
					<Button colorScheme='blue' onClick={handleState} >Sort by Price</Button>
				</Center>

				


				<Grid templateColumns="repeat(5, 1fr)" gap={4}>
					{
						mapData.map((product) => (
							<React.Fragment key={product.id}>
									
									<Box w="100%" key={product.id}>
										<Card item={product} />
									</Box>
								
							</React.Fragment>
					))}
				</Grid>
			</div>
		);
	}

	else {
		return (
			<div>
				<img src="https://cdn.dsmcdn.com/ty264/campaign/banners/original/592650/c170761659_0_new.jpg" width="100%" alt="" /> 
	
				<Center>
					<Button colorScheme='blue' onClick={handleState} >Sort by Price</Button>
				</Center>
				<Grid templateColumns="repeat(5, 1fr)" gap={4}>
					{
						data.map((product) => (
							<React.Fragment key={product.id}>
									
									<Box w="100%" key={product.id}>
										<Card item={product} />
									</Box>
								
							</React.Fragment>
					))}
				</Grid>
			</div>
		);
	}
	
}

export default Products;
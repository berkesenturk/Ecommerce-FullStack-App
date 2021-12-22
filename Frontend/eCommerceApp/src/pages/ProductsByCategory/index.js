import React, { useState } from "react";

import { Grid, Box, Button, Center, Radio, RadioGroup, Stack } from "@chakra-ui/react";
import { useQuery } from "react-query";

import { useParams } from "react-router-dom";
import { fetchProductList } from "../../api";
import Card from "../../components/Card";

function ProductsByCategory() {
	const {
		data,
		error,
		status,
	} = useQuery("products", fetchProductList);
	const { category_id } = useParams();
	
	const [sort, setSort] = useState(false)
	const [filterValue, setFilterValue] = useState(0)
	if (status === "loading") return "Loading...";
	if (status === "error") return "An error has occurred: " + error.message;
	
	let categoryData = [];

	function filterById() {
		return data.filter(function (item) {
			return item.category.id == category_id
		})
	}
	categoryData = filterById()

	

	let dataInit = categoryData.slice()
	let mapData=dataInit.sort(function(a, b) { return a.price - b.price })
	
	const handleState = () => {
		setSort(!sort)
	};

	const handleFilterValue = () => {
		setFilterValue('1000')

	}
	function filterByPrice() {
		return data.filter( data.filter(item => item.price<filterValue))
		
	}
	if (sort) {
		
		return (
			<div>
				<img src="https://cdn.dsmcdn.com/ty264/campaign/banners/original/592650/c170761659_0_new.jpg" width="100%" alt="" /> 

				<Center>
					<Button colorScheme='blue' onClick={handleState} >Sort by Price</Button>
				</Center>
				
				{/* <div onChange={h}>

				</div> */}
				<RadioGroup defaultValue='2'>
	 			<Stack spacing={5} direction='row'>
	 				<Radio colorScheme='red' value={filterValue ==='1000'} onClick={handleFilterValue}>
	 				Above 1000
	 				</Radio>
	 				<Radio colorScheme='green' value='5000' onClick={filterByPrice}>
	 				Above 5000
	 				</Radio>
	 				<Radio colorScheme='green' value='0' onClick={filterByPrice}>
	 				All
	 				</Radio>
	 			</Stack>
	 			</RadioGroup>

				<Grid templateColumns="repeat(5, 1fr)" gap={4}>
					{mapData.map((product, i) => (
						<React.Fragment key={i}>
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
					{categoryData.map((product, i) => (
						<React.Fragment key={i}>
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

export default ProductsByCategory;

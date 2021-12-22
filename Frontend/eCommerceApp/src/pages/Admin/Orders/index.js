import React from "react";

import {
	Table,
	Thead,
	Tbody,
	Tr,
	Th,
	Td,
	TableCaption,
	Text,
} from "@chakra-ui/react";
import { useQuery } from "react-query";
import { fetchOrders } from "../../../api";

function Orders() {
	const { isLoading, isError, data, error } = useQuery(
		"admin:orders",
		fetchOrders
	);

	if (isLoading) {
		return <div>Loading...</div>;
	}

	if (isError) {
		return <div>Error {error.message}</div>;
	}
	return (
		<div>
			<Text fontSize="2xl" p={5}>
				Orders
			</Text>

			<Table variant="simple">
				<TableCaption>Imperial to metric conversion factors</TableCaption>
				<Thead>
					<Tr>
						<Th>Name</Th>
						<Th>Address</Th>
						<Th>Price</Th>
						{/* ITEMS PERŞEMBEEE */}
						<Th isNumeric>Items</Th>
						<Th isNumeric>Phone</Th>
						<Th isNumeric>Postal Code</Th>
					</Tr>
				</Thead>
				<Tbody>
					{/* EMAIL EKLENCEK PERŞEMBE GÜNÜ */}
					{data.map((item) => (
						
						<Tr key={data.indexOf(item)+1}>
							{/* <Td>{item.email}</Td> */}
							<Td>{item.client.firstName} {item.client.lastName}</Td>
							<Td>{item.client.addressLine}</Td>
							<Td>{item.totalPrice}</Td>
							<Td>{item.client.phone}</Td>
							<Td>{item.client.postalCode}</Td>
							{/* <Td isNumeric>{item.items.length}</Td> */}
						</Tr>
					))}
				</Tbody>
			</Table>
		</div>
	);
}

export default Orders;

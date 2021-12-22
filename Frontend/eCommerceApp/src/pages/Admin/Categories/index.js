import { useMemo } from "react";

import { useQuery, useMutation, useQueryClient } from "react-query";
import { deleteCategory, fetchCategoryList } from "../../../api";

import { Link } from "react-router-dom";
import { Text, Button, Flex } from "@chakra-ui/react";
import { Table, Popconfirm } from "antd";

function Products() {
	const queryClient = useQueryClient();

	const { isLoading, isError, data, error } = useQuery(
		"admin:categories",
		fetchCategoryList
	);

	const deleteMutation = useMutation(deleteCategory, {
		onSuccess: () => queryClient.invalidateQueries("admin:categories"),
	});

	const columns = useMemo(() => {
		return [
			{
				title: "Name",
				dataIndex: "name",
				key: "name",
			},
			{
				title: "Action",
				key: "action",
				render: (text, record) => (
					<>	
						<Link to={`/admin/categories/${record.id}`}>Edit</Link>
						<Popconfirm
							title="Are you sure?"
							onConfirm={() => {
								deleteMutation.mutate(record.id, {
									onSuccess: () => {
										console.log("success");
									},
								});
							}}
							onCancel={() => console.log("iptal edildi")}
							okText="Yes"
							cancelText="No"
							placement="left"
						>
							<a href="/#" style={{ marginLeft: 10 }}>
								Delete
							</a>
						</Popconfirm>
					</>
				),
			},
		];
	}, []);

	if (isLoading) {
		return <div>Loading...</div>;
	}

	if (isError) {
		return <div>Error {error.message}</div>;
	}

	return (
		<div>
			<Flex justifyContent="space-between" alignItems="center">
				<Text fontSize="2xl" p="5">
					Categories
				</Text>

				<Link to="/admin/categories/new">
					<Button>New</Button>
				</Link>
			</Flex>

			<Table dataSource={data} columns={columns} rowKey="_id" />
		</div>
	);
}

export default Products;

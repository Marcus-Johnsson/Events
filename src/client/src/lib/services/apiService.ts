const apiUrl = import.meta.env.VITE_API_URL;

interface AppError {
	code: string;
	message: string;
	details?: unknown;
}

class ApiService {
	public async post<T,D>(endpoint: string, data: D): Promise<T | AppError> {
		try {
			const url = `${apiUrl.replace(/\/$/, '')}/${endpoint.replace(/^\//, '')}`;
			const response = await fetch(url, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(data)
			});
			const responseData = await response.json();
			return responseData;
		} catch (error) {
			return {
				code: 'NETWORK_ERROR',
				message: `Network error while posting to ${apiUrl}${endpoint}`,
				details: error
			};
		}
    }

	public async get<T>(
		endpoint: string,
		params?: Record<string, string | number | undefined>
	): Promise<T | AppError> {
		try {
			const url = `${apiUrl.replace(/\/$/, '')}/${endpoint.replace(/^\//, '')}`;
			const response = await fetch(url, {});
			if (!response.ok) {
				if (response.status === 404) {
					return {
						code: 'NOT_FOUND',
						message: `Resource not found at ${url}`,
						details: await response.text()
					};
				}
				return {
					code: 'API_ERROR',
					message: `Failed to get from ${url}`,
					details: await response.text()
				};
			}
			const responseData = await response.json();
			return responseData;
		} catch (error) {
			return {
				code: 'NETWORK_ERROR',
				message: `Network error while getting from ${apiUrl}${endpoint}`,
				details: error
			};
		}
	}

	public async put<T, D>(endpoint: string, data: D): Promise<T | AppError> {
		try {
			const url = `${apiUrl.replace(/\/$/, '')}/${endpoint.replace(/^\//, '')}`;
			const response = await fetch(url, {
				method: 'PUT',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(data)
			});
			if (!response.ok) {
				const responseData = await response.json();
				return responseData;
			}
			const responseData = await response.json();
			return responseData;
		} catch (error) {
			return {
				code: 'NETWORK_ERROR',
				message: `Network error while patching to ${apiUrl.replace(/\/$/, '')}/${endpoint.replace(/^\//, '')}`,
				details: error
			};
		}
	}

	public async delete<T>(endpoint: string): Promise<T | AppError> {
		try {
			const url = `${apiUrl.replace(/\/$/, '')}/${endpoint.replace(/^\//, '')}`;
			const response = await fetch(url, {
				method: 'DELETE',
			});
			if (!response.ok) {
				if (response.status === 404) {
					return {
						code: 'NOT_FOUND',
						message: `Resource not found at ${url}`,
						details: await response.text()
					};
				}
			}
			// Handle 204 No Content response so that we don't get an error after successful deletion
			if (response.status === 204) {
				return {} as T;
			}
			const responseData = await response.json();
			return responseData;
		} catch (error) {
			return {
				code: 'NETWORK_ERROR',
				message: `Network error while deleting ${apiUrl}${endpoint}`,
				details: error
			};
		}
	}
	
}

export default ApiService;
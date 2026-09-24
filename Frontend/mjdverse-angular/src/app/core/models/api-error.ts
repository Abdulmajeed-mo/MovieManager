export interface ApiError {
  status: number;
  message: string;
  errors?: string[];
}
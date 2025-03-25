export interface Doctor {
    id: number;
    name: string;
    contact: string;
    email: string;
    address: string;
    region?: string;  // Optional field
    country: string;
    postalCode?: string;  // Optional field
    imageUrl?: string;  // Optional field
    specialization: string;
  }
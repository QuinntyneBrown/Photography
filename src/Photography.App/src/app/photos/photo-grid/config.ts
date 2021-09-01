import { Photo } from "@api";

export interface GridConfig {
  photos: Photo[];
  columns: number;
  columnWidth: number;
  margin: number;
}

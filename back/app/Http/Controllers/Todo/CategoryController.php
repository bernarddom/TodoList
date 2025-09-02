<?php

namespace App\Http\Controllers\Todo;

use App\Http\Controllers\Controller;
use App\Models\Category;
use App\Services\CategoryService;
use Exception;
use Illuminate\Http\Request;

class CategoryController extends Controller
{
    private $service;
    private $storeRules = [
        'name'          => 'required|max:255',
        // 'description'   => 'required',
    ];
    function __construct(Category $category)
    {
        $this->service = new CategoryService($category);
    }
    public function store(Request $request)
    {
        $validated = $request->validate($this->storeRules);
        if (!$validated) {
            return response()
                ->json([
                    "status" => "error",
                    "error" => $validated
                ])
                ->setStatusCode(400)
                ;
        }
        try {
            $data = $request->all();
            if(isset($data["id"])) {
                $data = $this->service->update($data["id"], $data);
            } else {
                $data = $this->service->store($data);
            }
            return response()
                ->json([
                    "status" => "success",
                    "data" => $validated
                ])
                ->setStatusCode(200);
        } catch (Exception $e) {
            return response()
                ->json([
                    "status" => "error",
                    "error" => $e->getMessage()
                ])
                ->setStatusCode(400);
        }
    }

    public function getCategoryById(int $id) {
        try{
            return response()
                ->json([
                    "status" => "success",
                    "data" => $this->service->find($id)
                ])
                ->setStatusCode(200);
        } catch(Exception $e) {
            return response()
                ->json([
                    "status" => "error",
                    "error" => $e->getMessage()
                ])
                ->setStatusCode(400);
        }
    }
    public function getCategories()
    {
        return $this->service->all();
    }
}
